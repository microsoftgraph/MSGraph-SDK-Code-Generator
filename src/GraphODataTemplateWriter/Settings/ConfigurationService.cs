// Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

namespace Microsoft.Graph.ODataTemplateWriter.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;
    using Vipr.Core;

    /// <summary>
    /// The ConfigurationService configures the template writer with the target language and 
    /// other properties used to direct how the code files are generated.
    /// </summary>
    public static class ConfigurationService
    {
        private static IConfigurationProvider _configurationProvider;
        private static TemplateWriterSettings templateWriterSettings = null;
        private static string targetLanguage = null;
        private static Dictionary<string, string> properties = null;

        public static void Initialize(IConfigurationProvider configurationProvider, string targetLanguage = null, IEnumerable<string> properties = null)
        {
            _configurationProvider = configurationProvider;
            if (!String.IsNullOrEmpty(targetLanguage))
            {
                ConfigurationService.targetLanguage = targetLanguage;
            }
            if (properties != null)
            {
                Dictionary<string, string> propertyDictionary = new Dictionary<string, string>();
                foreach (string property in properties)
                {
                    string[] props = property.Split(':');

                    if (props.Length != 2)
                    {
                        throw new ArgumentException("A property was set in a unexpected form from the typewriter commandline.", "-p -properties");
                    }

                    propertyDictionary.Add(props[0],props[1]);
                }

                ConfigurationService.properties = propertyDictionary;
            }
        }

        private static TemplateWriterSettings LoadSettingsForLanguage()
        {
            TemplateWriterSettings mainTWS = _configurationProvider.GetConfiguration<TemplateWriterSettings>();

            if (targetLanguage != null)
            {
                mainTWS.TargetLanguage = targetLanguage;
            }
            if (properties != null)
            {
                mainTWS.Properties = properties;
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
                if (templateWriterSettings == null || templateWriterSettings.TargetLanguage != ConfigurationService.targetLanguage)
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
