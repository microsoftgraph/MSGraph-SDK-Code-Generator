[vipr-source-repo]: https://github.com/microsoft/vipr

[![Build status](https://o365exchange.visualstudio.com/O365%20Sandbox/_apis/build/status/Microsoft%20Graph/msgraph-package-typewriter)](https://o365exchange.visualstudio.com/O365%20Sandbox/_build/latest?definitionId=1728)

# Microsoft Graph SDK Code Generator

Source code writers for [VIPR][vipr-source-repo] utilizing T4 templates. The GraphODataTemplateWriter receives an OdcmModel from VIPR and uses it to fill in a T4 template located within this repository.

Currently the following target languages are supported by this writer:
- Android
- CSharp
- Java
- Objective-C
- Python
- TypeScript
- PHP

# Contents
- [Prerequisites](#prerequisites)
- [Getting started](#getting-started)
- [Using Vipr with this writer](#using-vipr-with-this-writer)
- [Contributing](#contributing)
- [License](#license)

## Prerequisites
- [Visual Studio SDK](https://msdn.microsoft.com/en-us/library/bb166441.aspx)
- [Visual Studio Modeling SDK](https://msdn.microsoft.com/en-us/library/bb126259.aspx)

# Getting started

This project uses git submodules to integrate upstream dependencies, specifically [Vipr][vipr-source-repo]. If you need an alternate branch to include special fixes you'll need to check that out manually within the submodule.

For the solution to open properly, ensure submodules are updated before opening it in Visual Studio. When initially cloning this repo, use `git clone --recursive` to update submodules at the same time. Later, run `git submodule update` to manually update submodules. If you don't use the `--recursive` switch when cloning, run `git submodule init` first to initialize the submodule.

Once setup is complete, you can work with the GraphODataTemplateWriter solution as usual. If you encounter problems, make sure NuGet packages and project references are all up-to-date.

For more information on submodules read [this chapter](http://git-scm.com/book/en/v2/Git-Tools-Submodules) from the Git book and search the Web.

## Using Typewriter

Typewriter is a new solution for generating code files using the GraphODataTemplateWriter and VIPR. It is an executable that is intended to simplify the generation of code files. Build the solution to find the typewriter executable in `\MSGraph-SDK-Code-Generator\src\Typewriter\bin\Release`. The typewriter run options are:

* **-l**, **-language**:  The target language for the generated code files. The values can be: `Android`, `Java`, `ObjC`, `CSharp`, `PHP`, `Python`, `TypeScript`, or `GraphEndpointList`. The default value is `CSharp`. This is not applicable when only generating clean and annotated metadata as specified by the `-generationmode Metadata` option.
* **-m**, **-metadata**: The local file path or URL to the target input metadata. The default value is `https://graph.microsoft.com/v1.0/$metadata`. This value is required.
* **-v**, **-verbosity**: The log verbosity level. The values can be: `Minimal`, `Info`, `Debug`, or `Trace`. The default value is `Minimal`.
* **-o**, **-output**: Specifies the path to the output folder. The default value is the directory that contains typewriter.exe. The structure and contents of the output directory will be different based on the `-generationmode` and `-language` options.
* **-d**, **-docs**: Specifies the path to the local root of the [microsoft-graph-docs](https://github.com/microsoftgraph/microsoft-graph-docs) repo. The default value is the directory that contains typewriter.exe. The documentation is parsed to provide documentation annotations to the metadata which is then used to add doc comments in the generated code files. This option is required when using `-generationmode` values of `Metadata` or `Full`.
* **-g**, **-generationmode**: Specifies the generation mode. The values can be: `Full`, `Metadata`, or `Files`. `Full` (default) generation mode produces the output code files by cleaning the input metadata, parsing the documentation, and adding annotations before generating the output files. `Metadata` generation mode produces an output metadata file by cleaning metadata, documentation parsing, and adding documentation annotations. `Files` generation mode produces code files from an input metadata and bypasses the cleaning, documentation parsing, and adding documentation annotations.
* **-f**, **-outputMetadataFileName**: The base output metadata filename. Only applicable for `-generationmode Metadata`. The default value is `cleanMetadataWithDescriptions` which is used with the value of the `-endpointVersion` to generate a metadata file named `cleanMetadataWithDescriptionsv1.0.xml`.
* **-e**, **-endpointVersion**: The endpoint version used when naming a metadata file. Expected values are `v1.0` and `beta`. Only applicable for `-generationmode Metadata`.
* **-p**, **-properties**: Specify properties to support generation logic in the T4 templates. Properties must take the form of *key-string:value-string*. Multiple properties can be specified by setting a space in between property. The only property currently supported is the *php.namespace* property to specify the generated model file namespace. This property is optional.

### Example typewriter usage

#### Generate TypeScript typings from a CSDL (metadata) file without cleaning or annotating the CSDL.

The output will go in to the `outputTypeScript` directory.

`.\typewriter.exe -v Info -m D:\cleanMetadataWithDescriptions_v10.xml -o outputTypeScript -l TypeScript -g Files`

#### Clean and annotate a metadata file with documentation annotations sourced from the documentation repo

The output metadata file will go in to the `output2` directory. The output metadata file will be named `cleanMetadataWithDescriptionsv1.0.xml` based on the default values.

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output2 -d D:\repos\microsoft-graph-docs -g Metadata`

#### Generate C# code files from the metadata that will be cleaned and annotated with documentation annotations sourced from the documentation repo

The output C# code files will go in to the `output` directory.

`.\typewriter.exe -v Info -m D:\v1.0_2018_10_23_source.xml -o output -l CSharp -d D:\repos\microsoft-graph-docs -g Full`



## Using Vipr with this Writer

1. Build the solution in Visual Studio.
2. Go to the `src\GraphODataTemplateWriter\bin\debug` folder to find all compiled components.
3. In that folder, modify `.config\TemplateWriterSettings.json` to specify your template mapping see [Template Writer Settings](##Template-Writer-Settings) for more details.
4. Open a command prompt as administrator in the same folder and run `Vipr.exe <path-or-url-to-metadata> --writer="GraphODataTemplateWriter"`.  An example metadata file can be found in the root of this project.

By default, output source code will be put in a folder named "output" next to the Vipr executable.

## Template Writer Settings
### Available Languages

There are five languages to choose from at the moment.  Java, ObjC, CSharp, TypeScript and Python.  Specify which language you want to generate in the `TargetLanguage` setting.

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

## Building against Graph Metadata

There are currently several steps we take to form the metadata into one that will successfully generate SDKs in the shape we expect:

  - Remove capability annotations (see [#132](https://github.com/Microsoft/Vipr/issues/132))
  - Add navigation annotation to thumbnail
     ```xml
     <Annotation String="navigable" Term="Org.OData.Core.V1.LongDescription"/>
     ```
  - Remove HasStream properties from ```onenotePage``` and ```onenoteEntityBaseModel```
  - Add ```ContainsTarget="true"``` to navigation properties that do not have a corresponding EntitySet. This currently applies to navigation properties that contain plannerBucket, plannerTask, plannerPlan, and plannerDelta.
  - Add long descriptions to types and properties from [docs](https://developer.microsoft.com/en-us/graph/docs/concepts/overview)

In order to build against metadata other than that stored in the [metadata](https://github.com/microsoftgraph/MSGraph-SDK-Code-Generator/tree/master/metadata) directory, you will need to perform the first four on this list.

## Contributing

Before we can accept your pull request, you'll need to electronically complete Microsoft's [Contributor License Agreement](https://cla.microsoft.com/). If you've done this for other Microsoft projects, then you're already covered.

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

[Why a CLA?](https://www.gnu.org/licenses/why-assign.html) (from the FSF)

## License

Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the [MIT license](LICENSE).
