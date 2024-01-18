param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [string]
    $packageName = "@microsoft/msgraph-sdk-javascript",
    [string]
    $kiotaPath = "kiota",
    [string]
    $descriptionPath = "https://aka.ms/graph/v1.0/openapi.yaml",
    [string]
    $additionalArguments = ""
)
$modelsPackageDirectoryName = $packageName.Split('/')[1]
$rootSegments = Get-ChildItem -Directory -Exclude $modelsPackageDirectoryName -Path $targetDirectory | Select-Object -ExpandProperty Name | % { $_.Substring($modelsPackageDirectoryName.Length + 1)}
foreach($rootSegment in $rootSegments) {
    $fluentAPIPackageDirectoryPath = "$targetDirectory/$modelsPackageDirectoryName-$rootSegment"
    . "$kiotaPath generate -o $fluentAPIPackageDirectoryPath -d $descriptionPath -c $($rootSegment.Substring(0,1).ToUpper() + $rootSegment.Substring(1))ServiceClient -l TypeScript -n github.com/microsoftgraph/msgraph-sdk-typescript/ -i '/$rootSegment' -i '/$rootSegment/**'$additionalArguments"
    .\scripts\clean-typescript-fluent-package.ps1 -targetDirectory $fluentAPIPackageDirectoryPath -packageName $packageName
}
. "$kiotaPath generate -o $fluentAPIPackageDirectoryPath -d $descriptionPath -c GraphServiceClient -l TypeScript -n github.com/microsoftgraph/msgraph-sdk-typescript/ $additionalArguments"
.\scripts\clean-typescript-main-package.ps1 -targetDirectory $targetDirectory