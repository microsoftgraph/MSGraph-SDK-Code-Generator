# VIPR T4 Writer

Source code writers for [VIPR](https://github.com/microsoft/vipr) utilizing T4 templates. The TemplateWriter receives an OdcmModel from VIPR and uses it to fill in a T4 template.

Currently the following target languages are supported:
- Java (for Android and JVM)
- Objective-C (for iOS)

Generated code depends on an underlying HTTP client and other services. These are available for [Android and JVM](https://github.com/officedev/office-365-sdk-for-android) and [iOS](https://github.com/officedev/office-365-sdk-for-ios).

## Get started

This project uses git submodules to fullfil Vipr depdendencies.
Please follow the following instructions:

```
git clone --recursive git@github.com:MSOpenTech/vipr-t4-writer.git
git submodule init
git submodule update
```

At this point you have the source code for vipt t4 writer and Vipr source code.
Now, we need to build Vipr to retrieve the NuGet packages:

```
cd submodule\Vipr\build.cmd
```

Now, we proceed to open the vipr-t4-writer as any regular Visual Studio Solution.

> Note: When nuget packages are available for VIPR core we'll replace the script with them.

## License

This project is licensed under the [MIT license](LICENSE).
