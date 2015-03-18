# VIPR T4 Writer

Source code writers for [VIPR](https://github.com/microsoft/vipr) utilizing T4 templates. The T4TemplateWriter receives an OdcmModel from VIPR and uses it to fill in a T4 template.

Currently the following target languages are supported:
- Java (for Android and JVM)
- Objective-C (for iOS)

Generated code depends on an underlying HTTP client and other services. These are available for [Android and JVM](https://github.com/officedev/office-365-sdk-for-android) and [iOS](https://github.com/officedev/office-365-sdk-for-ios).

## Getting started

This project uses git submodules to integrate upstream dependencies. When cloning this repo, use `git clone --recursive` to update submodules at the same time.

If you clone without the "--recursive" switch, run `git submodule update --init` to manually update submodules.

You can build Vipr and its dependencies by executing `.\submodule\Vipr.build.cmd` from a Windows command prompt.

You can now work with the vipr-t4-writer solution as usual.

> Note: We will integrate Vipr via public NuGet packages when these become available.

For more information on submodules read [this chapter](http://git-scm.com/book/en/v2/Git-Tools-Submodules) from the Git book and search the Web.

## License

This project is licensed under the [MIT license](LICENSE).
