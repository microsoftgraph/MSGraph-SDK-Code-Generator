[vipr-source-repo]: https://github.com/microsoft/vipr

# VIPR T4 Template Writer

[![Build status](https://ci.appveyor.com/api/projects/status/8o1e5efpqp957kd3?svg=true)](https://ci.appveyor.com/project/joshgav/vipr-t4templatewriter-jqj22)

Source code writers for [VIPR][vipr-source-repo] utilizing T4 templates. The T4TemplateWriter receives an OdcmModel from VIPR and uses it to fill in a T4 template.

Currently the following target languages are supported by this writer:
- Android
- Java for JVM
- Objective-C for iOS

# Contents
- [Prerequisites](#Prerequisites)
- [Getting started](#Getting-started)
- [Using Vipr with this writer](#Using-Vipr-with-this-Writer)
- [Using generated code](#Using-generated-code)
- [Contributing](#Contributing)
- [License](#License)

## Prerequisites
- [Visual Studio SDK](https://www.microsoft.com/en-us/download/details.aspx?id=40758)
- [Visual Studio Modeling SDK](https://www.microsoft.com/en-us/download/details.aspx?id=40754)

# Getting started

This project uses git submodules to integrate upstream dependencies, specifically [Vipr][vipr-source-repo]. This repo will point to [msopentech/vipr/int-msot](https://github.com/msopentech/vipr/tree/int-msot) by default; if you need an alternate branch to include special fixes you'll need to check that out manually within the submodule.

For the solution to open properly, ensure submodules are updated before opening it in Visual Studio. When initially cloning this repo, use `git clone --recursive` to update submodules at the same time. Later, run `git submodule update` to manually update submodules. If you don't use the `--recursive` switch when cloning, run `git submodule init` first to initialize the submodule.

Once setup is complete, you can work with the vipr-t4templatewriter solution as usual. If you encounter problems, make sure NuGet packages and project references are all up-to-date.

> Note: We will consider integrating Vipr via public NuGet packages when these become available.

For more information on submodules read [this chapter](http://git-scm.com/book/en/v2/Git-Tools-Submodules) from the Git book and search the Web.

## Using Vipr with this Writer

1. Build the solution in Visual Studio.
2. Go to the `src\T4TemplateWriter\bin\debug` folder to find all compiled components.
3. In that folder, modify `.config\TemplateWriterSettings.json` to specify your template mapping see [Template Writer Settings](##Template-Writer-Settings) for more details.
4. Open a command prompt as administrator in the same folder and run `Vipr.exe <path-or-url-to-metdata> --writer="Vipr.T4TemplateWriter"`.

By default, output source code will be put in a folder named "output" next to the Vipr executable.

## Template Writer Settings
### Available Languages

There are 4 languages to choose from at the moment.  Java, ObjC, CSharp, and Python.  Specify which language you want to generate in the `TargetLanguage` setting.

### Templates
You must specify a template directory under the `TemplatesDirectory` Settings.  The directory can be a full path or relative to the running directory.  The directory must contain a sub directory for each platform you want to generate code for. See the Templates directory for an example.

### Template Mapping
You must specify the mapping of T4 Templates to specific SubProcessors for each platform you wish to generate.  The `TemplateMapping` setting is a dictionary of languages and list of templates.  Each template must specify :

- `Template`, the name of the template without the extensions.
- `SubProcessor` The SubProcessor for the template see [SubProcessors](#SubProcessors)
- `Type` The type of template.
- `Name` The format string for the name.

and optionally :

- `Include`, a semicolon delimited list of objects to include in the subprcoessor.
- `Exclude`, a semicolon delimited list of objects to exclude from the subprcoessor.
- `Ignore`, a semicolon delimited list of objects to ignore from the subprcoessor.
- `Matches`, a semicolon delimited list of objects to include in the subprcoessor.
- `FileCasing`, `UpperCamel`, `LowerCamel` or `Snake` for the file casing for the specific file being created.

Example :

` { "Template": "EntityType", "SubProcessor": "EntityType", "Type": "Model", "Name": "<Class>", "Matches" : "includeThisType", "Exclude" : "ExcludedTypeName;OtherExcludedTypeName" }`

#### SubProcessors

The SubProcessors determine what type of OData object will be passed into the template generating the code file.

- `EntityType` All Entity Types
- `ComplexType` All Complex Types
- `EnumType` All Enum Types
- `Property` All Properties
- `EntityContainer` The EntityContainer
- `CollectionProperty` Properties that are of type collection
- `Method` All Actions and Functions
- `NonCollectionMethod` All methods that do not return a collection
- `CollectionMethod` All methods that return collections
- `MethodWithBody` All methods that will contain a body in the http request
- `Other` The entire model.

#### Types

The type of template.

- `Request` A template that will make a request
- `Model` A model
- `Shared` A template that will not output any code but is included by other templates
- `Client` The template used to create the Client object
- `Other` Any other type


#### Template Name

To set the name of the template using the `Name` format string. You can insert `<Class>`, `<Property>`, `<Method>`, and `<Container>` the values will be replaced by the names of the corresponding object.  If you insert an item that doesn't exist it will be replaced with an empty string.  
Note: You can also set the template name from inside the template by : `host.SetTemplateName("foo");` 

#### Includes/Excludes

There may be specific times when you want to exclude or only process certain objects from the SubProcessor. To Do this you can either set a semicolon delimited list of objects you wanted to include : `Include : foo;bar`. This will only process objects whose names are foo or bar.  The opposite of this is the exclude setting where the SubProcessor will include all objects except for those whose names are in the exclude list, exclude and include can not be used together.

#### Ignore/Matches
When you can't use the name of an object to include or exclude you can use the long description element on any object.  Insert a long description with a semicolon delimited list of strings like : `foo;bar;baz`.  If you add a `"Matches" : "foo;baz"` only objects who contain foo and baz in their long description will be processed.  The opposite is true for Ignore.

Note: You can also check in a template by `odcjObject.LongDescriptionContains("foo");`



# Using generated code

The default generated code depends on an underlying HTTP client and other services. These are available for [Android and JVM][android-sdk-folder] ([odata-engine-core][], [odata-engine-android-impl][], [odata-engine-jvm-impl][]) and [iOS][ios-sdk-folder] ([office365_odata_base][])

[android-sdk-folder]: https://github.com/OfficeDev/Office-365-SDK-for-Android/tree/master/sdk
[ios-sdk-folder]: https://github.com/officedev/office-365-sdk-for-ios/tree/master/sdk-objectivec
[odata-engine-core]: https://github.com/OfficeDev/Office-365-SDK-for-Android/tree/master/sdk/odata-engine-core
[odata-engine-android-impl]: https://github.com/OfficeDev/Office-365-SDK-for-Android/tree/master/sdk/odata-engine-android-impl
[odata-engine-jvm-impl]: https://github.com/OfficeDev/Office-365-SDK-for-Android/tree/master/sdk/odata-engine-jvm-impl
[office365_odata_base]: https://github.com/OfficeDev/Office-365-SDK-for-iOS/tree/master/sdk-objectivec/office365_odata_base

## Contributing

Before we can accept your pull request, you'll need to electronically complete Microsoft Open Tech's [Contributor License Agreement](https://cla.msopentech.com/). If you've done this for other Microsoft Open Tech projects, then you're already covered.

[Why a CLA?](https://www.gnu.org/licenses/why-assign.html) (from the FSF)

## License

Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. Licensed under the [MIT license](LICENSE).
