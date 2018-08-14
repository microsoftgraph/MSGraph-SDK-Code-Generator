// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.Settings
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using Vipr.Core;

    public static class ConfigurationService
    {
        private static IConfigurationProvider _configurationProvider;
        private static TemplateWriterSettings templateWriterSettings = null;
        private static string targetLanguage = null;
        public static void Initialize(IConfigurationProvider configurationProvider, string targetLanguage)
        {
            _configurationProvider = configurationProvider;
            if (!String.IsNullOrEmpty(targetLanguage))
            {
                ConfigurationService.targetLanguage = targetLanguage;
            }
        }

        private static TemplateWriterSettings LoadSettingsForLanguage()
        {
            TemplateWriterSettings mainTWS = _configurationProvider.GetConfiguration<TemplateWriterSettings>();

            if (targetLanguage != null)
            {
                mainTWS.TargetLanguage = targetLanguage;
            }

            TemplateWriterSettings.mainSettingsObject = mainTWS;
            
            //First dynamically create a new class that holds settings for the target language
            //We store a reference on the default constructor to the mainTWS and then copy
            //all properties on it.

            var targetLanguageTypeName = mainTWS.TargetLanguage + "Settings";
            var targetLanguageAN = new AssemblyName(targetLanguageTypeName);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(targetLanguageAN, AssemblyBuilderAccess.Run);
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");
            TypeBuilder tb = moduleBuilder.DefineType(targetLanguageTypeName
                                , TypeAttributes.Public
                                | TypeAttributes.Class
                                | TypeAttributes.AutoClass
                                | TypeAttributes.BeforeFieldInit
                                | TypeAttributes.AutoLayout
                                , typeof(TemplateWriterSettings));

            ConstructorBuilder ctor = tb.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);

            ILGenerator ctorIL = ctor.GetILGenerator();

            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Call, typeof(TemplateWriterSettings).GetConstructor(Type.EmptyTypes));
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Call, typeof(TemplateWriterSettings).GetMethod("CopyPropertiesFromMainSettings"));
            ctorIL.Emit(OpCodes.Ret);

            Type targetLanguageType = tb.CreateType();

            //Call the generic GetConfiguration method with our new type.
            return (TemplateWriterSettings)typeof(IConfigurationProvider)
                                            .GetMethod("GetConfiguration")
                                            .MakeGenericMethod(targetLanguageType)
                                            .Invoke(_configurationProvider, new object[] { });

        }

        public static TemplateWriterSettings Settings
        {
            get
            {
                if (templateWriterSettings == null)
                {
                    templateWriterSettings = _configurationProvider != null
                        ? LoadSettingsForLanguage()
                        : new TemplateWriterSettings();
                }

                return templateWriterSettings;
            }
        }
    }
}
