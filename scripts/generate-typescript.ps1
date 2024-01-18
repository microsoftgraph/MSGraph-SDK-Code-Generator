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
    $additionalArguments = "",
    [string]
    $clientName = "GraphBaseServiceClient"
)
$ENV:KIOTA_TUTORIAL_ENABLED = $false
$modelsPackageDirectoryName = $packageName.Split('/')[1]
$rootSegments = Get-ChildItem -Directory -Exclude $modelsPackageDirectoryName -Path $targetDirectory | Select-Object -ExpandProperty Name | % { $_.Substring($modelsPackageDirectoryName.Length + 1)}
foreach($rootSegment in $rootSegments) {
    $fluentAPIPackageDirectoryPath = "$targetDirectory/$modelsPackageDirectoryName-$rootSegment"
    Write-Host "generating segment $rootSegment"
    Invoke-Expression "$kiotaPath generate -o $fluentAPIPackageDirectoryPath -d $descriptionPath -c $($rootSegment.Substring(0,1).ToUpper() + $rootSegment.Substring(1))ServiceClient -l TypeScript -n github.com/microsoftgraph/msgraph-sdk-typescript/ -e '/me' -e '/me/**' -i '/$rootSegment' -i '/$rootSegment/**'$additionalArguments"
    .\scripts\clean-typescript-fluent-package.ps1 -targetDirectory $fluentAPIPackageDirectoryPath -packageName $packageName
}
$modelsPackagePath = "$targetDirectory/$modelsPackageDirectoryName"
Invoke-Expression "$kiotaPath generate -o $modelsPackagePath -d $descriptionPath -c $clientName -l TypeScript -n github.com/microsoftgraph/msgraph-sdk-typescript/ -e '/me' -e '/me/**' $additionalArguments"
.\scripts\clean-typescript-main-package.ps1 -targetDirectory $modelsPackagePath