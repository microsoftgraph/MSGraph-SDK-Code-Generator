param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [string]
    $packageName = "@microsoft/msgraph-sdk-javascript",
    [string]
    $kiotaPath = "kiota",
    [string]
    $descriptionPath = "https://aka.ms/graph/v1.0/openapi.yaml"
)
$modelsPackageDirectoryName = $packageName.Split('/')[1]
$fluentAPIPackageDirectoryNames = Get-ChildItem -Directory -Exclude $modelsPackageDirectoryName -Path $targetDirectory | Select-Object -ExpandProperty Name | % { $_.Substring($modelsPackageDirectoryName.Length + 1)}
foreach($fluentAPIPackageDirectoryName in $fluentAPIPackageDirectoryNames) {
    $fluentAPIPackageDirectoryPath = "$targetDirectory/$modelsPackageDirectoryName-$fluentAPIPackageDirectoryName"
    . "$kiotaPath generate -o $fluentAPIPackageDirectoryPath -d $descriptionPath -c $($fluentAPIPackageDirectoryName.Substring(0,1).ToUpper() + $fluentAPIPackageDirectoryName.Substring(1))ServiceClient -l TypeScript -n github.com/microsoftgraph/msgraph-sdk-typescript/ -i '/$fluentAPIPackageDirectoryName' -i '/$fluentAPIPackageDirectoryName/**'"
    .\scripts\clean-typescript-fluent-package.ps1 -targetDirectory $fluentAPIPackageDirectoryPath -packageName $packageName
}