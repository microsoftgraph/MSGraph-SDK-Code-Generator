[vipr-source-repo]: https://github.com/microsoft/vipr

# Microsoft Graph SDK Code Generator

Source code writers for [VIPR][vipr-source-repo] utilizing T4 templates. The GraphODataTemplateWriter receives an OdcmModel from VIPR and uses it to fill in a T4 template located within this repository.

Currently the following target languages are supported by this writer:
- Android
- CSharp
- JavaScript
- Objective-C
- Python

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

This project uses git submodules to integrate upstream dependencies, specifically [Vipr][vipr-source-repo]. If you need an alternate branch to include special fixes you'll need to check that out manually within the submodule.

For the solution to open properly, ensure submodules are updated before opening it in Visual Studio. When initially cloning this repo, use `git clone --recursive` to update submodules at the same time. Later, run `git submodule update` to manually update submodules. If you don't use the `--recursive` switch when cloning, run `git submodule init` first to initialize the submodule.

Once setup is complete, you can work with the GraphODataTemplateWriter solution as usual. If you encounter problems, make sure NuGet packages and project references are all up-to-date.

For more information on submodules read [this chapter](http://git-scm.com/book/en/v2/Git-Tools-Submodules) from the Git book and search the Web.

## Using Vipr with this Writer

1. Build the solution in Visual Studio.
2. Go to the `src\T4TemplateWriter\bin\debug` folder to find all compiled components.
3. In that folder, modify `.config\TemplateWriterSettings.json` to specify your template mapping see [Template Writer Settings](##Template-Writer-Settings) for more details.
4. Open a command prompt as administrator in the same folder and run `Vipr.exe <path-or-url-to-metadata> --writer="GraphODataTemplateWriter"`

By default, output source code will be put in a folder named "output" next to the Vipr executable.

## Template Writer Settings
### Available Languages

There are four languages to choose from at the moment.  Java, ObjC, CSharp, and Python.  Specify which language you want to generate in the `TargetLanguage` setting.

### Templates
You must specify a template directory under the `TemplatesDirectory` Settings.  The directory can be a full path or relative to the running directory.  The directory must contain a sub directory for each platform you want to generate code for. See the Templates directory for an example.

### Template Mapping
You must specify the mapping of T4 Templates to specific SubProcessors for each platform you wish to generate.  The `TemplateMapping` setting is a dictionary of languages and list of templates.  Each template must specify :

- `Template`, the name of the template without the extensions.
- `SubProcessor` The SubProcessor for the template see [SubProcessors](#SubProcessors)
- `Type` The type of template.
- `Name` The format string for the name.

and optionally :

- `Include`, a semicolon delimited list of objects to include in the subprocessor.
- `Exclude`, a semicolon delimited list of objects to exclude from the subprocessor.
- `Ignore`, a semicolon delimited list of objects to ignore from the subprocessor.
- `Matches`, a semicolon delimited list of objects to include in the subprocessor.
- `FileCasing`, `UpperCamel`, `LowerCamel` or `Snake` for the file casing for the specific file being created.

**Note: Many of these optional parameters were used before Vipr had full support for annotations; now that annotations have been added to Vipr usage of these parameters should be limited to legacy scenarios**

Example :

` { "Template": "EntityType", "SubProcessor": "EntityType", "Type": "Model", "Name": "<Class>", "Matches" : "includeThisType", "Exclude" : "ExcludedTypeName;OtherExcludedTypeName" }`

#### SubProcessors

The SubProcessors determine what type of OData object will be passed into the template generating the code file.

- `CollectionMethod` All Methods that are of type Collections
- `CollectionProperty` Properties that are of type collection
- `CollectionReferenceProperty` All Navigation Properties that are of type Collection which are used in Non-Containment Collections
- `ComplexType` All Complex types
- `EntityContainer`  The EntityContainer
- `EntityReferenceType` All Entity types which are used in Non-Containment Collections
- `EntityType` All Entity types
- `EnumType` All Enumerable types
- `MediaEntityType` All Media Entity types
- `Method` All Actions and Functions
- `MethodWithBody` All Methods and Functions that send a body within the http request
- `NavigationCollectionProperty` All Navigation Properties that are of type Collection
- `NonCollectionMethod` All Methods and Functions that do not return a collection
- `Other` The entire model.
- `Property` All Properties types
- `StreamProperty` All Properties types that return a Streams

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

#### Template Editing

The solution contains a non-building project to host the actual T4 templates and make browsing/editing them easier.  New template files will be automatically discovered by this project.

#### Includes/Excludes

There may be specific times when you want to exclude or only process certain objects from the SubProcessor. To Do this you can either set a semicolon delimited list of objects you wanted to include : `Include : foo;bar`. This will only process objects whose names are foo or bar.  The opposite of this is the exclude setting where the SubProcessor will include all objects except for those whose names are in the exclude list, exclude and include can not be used together.

#### Ignore/Matches
When you can't use the name of an object to include or exclude you can use the long description element on any object.  Insert a long description with a semicolon delimited list of strings like : `foo;bar;baz`.  If you add a `"Matches" : "foo;baz"` only objects who contain foo and baz in their long description will be processed.  The opposite is true for Ignore.

Note: You can also check in a template by `odcjObject.LongDescriptionContains("foo");`

**Note: Includes/Excludes and Ignore/Matches were used before Vipr had full support for annotations; now that annotations have been added to Vipr usage of these parameters should be limited to legacy scenarios**

## Contributing

Before we can accept your pull request, you'll need to electronically complete Microsoft's [Contributor License Agreement](https://cla.microsoft.com/). If you've done this for other Microsoft projects, then you're already covered.

[Why a CLA?](https://www.gnu.org/licenses/why-assign.html) (from the FSF)

## License

Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the [MIT license](LICENSE).
