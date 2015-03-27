[vipr-source-repo]: https://github.com/microsoft/vipr

# VIPR T4 Template Writer

Source code writers for [VIPR][vipr-source-repo] utilizing T4 templates. The T4TemplateWriter receives an OdcmModel from VIPR and uses it to fill in a T4 template.

Currently the following target languages are supported by this writer:
- Android
- Java for JVM
- Objective-C for iOS

# Contents
- [Getting started](#Getting-started)
- [Using Vipr with this writer](#Using-Vipr-with-this-Writer)
- [Using generated code](#Using-generated-code)
- [Contributing](#Contributing)
- [License](#License)

## Getting started

This project uses git submodules to integrate upstream dependencies, specifically [Vipr][vipr-source-repo]. This repo will point to [msopentech/vipr/int-msot](https://github.com/msopentech/vipr/tree/int-msot) by default; if you need an alternate branch to include special fixes you'll need to check that out manually within the submodule.

For the solution to open properly, ensure submodules are updated before opening it in Visual Studio. When initially cloning this repo, use `git clone --recursive` to update submodules at the same time. Later, run `git submodule update` to manually update submodules. If you don't use the `--recursive` switch when cloning, run `git submodule init` first to initialize the submodule.

Once setup is complete, you can work with the vipr-t4templatewriter solution as usual. If you encounter problems, make sure NuGet packages and project references are all up-to-date.

> Note: We will consider integrating Vipr via public NuGet packages when these become available.

For more information on submodules read [this chapter](http://git-scm.com/book/en/v2/Git-Tools-Submodules) from the Git book and search the Web.

## Using Vipr with this Writer

1. Build the solution in Visual Studio.
2. Go to the `src\T4TemplateWriter\bin\debug` folder to find all compiled components.
3. In that folder, modify `.config\TemplateWriterSettings.json` to specify target language - either "java" or "objectivec".
4. Open a command prompt as administrator in the same folder and run `Vipr.exe <path-or-url-to-metdata> --writer="T4TemplateWriter"`.

By default, output source code will be put in a folder named "output" next to the Vipr executable.

## Using generated code

Generated code depends on an underlying HTTP client and other services. These are available for [Android and JVM][android-sdk-folder] ([odata-engine-core][], [odata-engine-android-impl][], [odata-engine-jvm-impl][]) and [iOS][ios-sdk-folder] ([office365_odata_base][])

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
