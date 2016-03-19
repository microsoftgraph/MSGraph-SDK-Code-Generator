// Copyright (c) Microsoft Open Technologies, Inc. All Rights Reserved.
// Licensed under the MIT License. See LICENSE in the source repository root for license information. 

namespace Vipr.T4TemplateWriter.Settings
{
    using System;
    using System.Reflection;
    using System.Reflection.Emit;
    using Vipr.Core;

    public static class ConfigurationService
    {
        private static IConfigurationProvider _configurationProvider;
        private static TemplateWriterSettings templateWriterSettings = null;

        public static void Initialize(IConfigurationProvider configurationProvider)
        {
            _configurationProvider = configurationProvider;
        }

        private static TemplateWriterSettings LoadSettingsForLanguage()
        {
            TemplateWriterSettings mainTWS = _configurationProvider.GetConfiguration<TemplateWriterSettings>();

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
