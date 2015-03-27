# VIPR T4TemplateWriter (Private Repo)

**This is a private repo for work intended for merge into the [public vipr-t4templatewriter](https://github.com/msopentech/vipr-t4templatewriter) repo.**

**This repo contains CSDL files for target APIs. It also contains initial work on T4 templates for code generation for Swift.**

Source code writers for [VIPR](https://github.com/microsoft/vipr) utilizing T4 templates. The T4TemplateWriter receives an OdcmModel from VIPR and uses it to fill in a T4 template.

Currently the following target languages are supported:
- Android
- Java
- Objective-C for iOS

Generated code depends on an underlying HTTP client and other services. These are available for [Android](https://github.com/officedev/office-365-sdk-for-android) and [iOS](https://github.com/officedev/office-365-sdk-for-ios).

## Getting started

This project uses git submodules to integrate upstream dependencies, specifically [Vipr](https://github.com/microsoft/vipr). This repo will point to [msopentech/vipr/int-msot](https://github.com/msopentech/vipr/tree/int-msot) by default; if you need an alternate branch to include special fixes you'll need to check that out manually within the submodule.

For the solution to open properly, ensure submodules are updated before opening it in Visual Studio. You can do so when initially cloning this repo, use `git clone --recursive` to update submodules at the same time. Later, run `git submodule update` to manually update submodules.

Once this setup is complete, you can work with the vipr-t4templatewriter solution as usual. If you encounter problems, make sure NuGet packages and project references are all up-to-date.

Note: We will consider integrating Vipr via public NuGet packages when these become available.

For more information on submodules read [this chapter](http://git-scm.com/book/en/v2/Git-Tools-Submodules) from the Git book and search the Web.

## Using Vipr with this Writer

1. Build the solution in Visual Studio.
2. Go to the `src\T4TemplateWriter\bin\debug` folder to find all compiled components.
3. In that folder, modify `.config\TemplateWriterSettings.json` to specify target language.
4. Open a command prompt as administrator in the same folder and run `vipr.exe <path-to-metdata> --writer="T4TemplateWriter"`.

By default, output source code will be put in the same folder as the Vipr executable.

## License

This project is licensed under the [MIT license](LICENSE).
