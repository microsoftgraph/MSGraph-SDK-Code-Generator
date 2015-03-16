# VIPR T4 Writer

Source code writers for [VIPR](https://github.com/microsoft/vipr) utilizing T4 templates. The TemplateWriter receives an OdcmModel from VIPR and uses it to fill in a T4 template.

Currently the following target languages are supported:
- Java (for Android and JVM)
- Objective-C (for iOS)

Generated code depends on an underlying HTTP client and other services. These are available for [Android and JVM](https://github.com/officedev/office-365-sdk-for-android) and [iOS](https://github.com/officedev/office-365-sdk-for-ios).

## Get started

From root folder of this repo, run prepDeps.cmd as an administrator. This clones the [VIPR source repo](https://github.com/microsoft/vipr), builds that project, and copies a couple DLLs to the `bin\debug` directory of this project.

After building this project, run `./vipr.exe "path_to_template.edmx" --writer=TemplateWriter` from the `bin\debug` folder to test.

> Note: When nuget packages are available for VIPR core we'll replace the script with them.

## License

This project is licensed under the [MIT license](LICENSE).
