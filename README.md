# VIPR T4 Writer

Source code writers for [VIPR](https://github.com/microsoft/vipr) utilizing T4 templates. The T4TemplateWriter receives an OdcmModel from VIPR and uses it to fill in a T4 template.

Currently the following target languages are supported:
- Java (for Android and JVM)
- Objective-C (for iOS)

Generated code depends on an underlying HTTP client and other services. These are available for [Android and JVM](https://github.com/officedev/office-365-sdk-for-android) and [iOS](https://github.com/officedev/office-365-sdk-for-ios).

## Getting started

This project uses git submodules to integrate upstream dependencies, and you must ensure submodules are updated before opening the solution in Visual Studio. When cloning this repo, use `git clone --recursive` to update submodules at the same time (Note that you may need to specify a specific branch with `-b <branchname>` as well).

If you clone without the "--recursive" switch, run `git submodule update --init` to manually update submodules.

You can now work with the vipr-t4-writer solution as usual. If you encounter problems, make sure NuGet packages and project references are all up-to-date.

Note: We will integrate Vipr via public NuGet packages when these become available.

For more information on submodules read [this chapter](http://git-scm.com/book/en/v2/Git-Tools-Submodules) from the Git book and search the Web.

## Using Vipr with this Writer

1. Build the solution in Visual Studio.
2. Go to the root of this repo to the `bin\debug` folder.
3. In that folder, modify `.config\TemplateWriterSettings.json` to specify target language and output folder.
4. Open a command prompt as administrator in the same folder and run `vipr.exe <path-to-metdata> --reader="T4TemplateWriter"`.

## License

This project is licensed under the [MIT license](LICENSE).
