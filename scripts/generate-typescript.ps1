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
$modelsPackageDirectoryName = $packageName.Split('/')[1]
$rootSegments = Get-ChildItem -Directory -Exclude $modelsPackageDirectoryName -Path $targetDirectory | Select-Object -ExpandProperty Name | % { $_.Substring($modelsPackageDirectoryName.Length + 1)}
$rootSegments | Foreach-Object -ThrottleLimit 10  -Parallel {
    $fluentAPIPackageDirectoryPath = "$targetDirectory/$modelsPackageDirectoryName-$_"
    Invoke-Expression "$kiotaPath generate -o $fluentAPIPackageDirectoryPath -d $descriptionPath -c $($_.Substring(0,1).ToUpper() + $_.Substring(1))ServiceClient -l TypeScript -n github.com/microsoftgraph/msgraph-sdk-typescript/ -e '/me' -e '/me/**' -i '/$_' -i '/$_/**'$additionalArguments"
    .\scripts\clean-typescript-fluent-package.ps1 -targetDirectory $fluentAPIPackageDirectoryPath -packageName $packageName
}
Invoke-Expression "$kiotaPath generate -o $fluentAPIPackageDirectoryPath -d $descriptionPath -c $clientName -l TypeScript -n github.com/microsoftgraph/msgraph-sdk-typescript/ -e '/me' -e '/me/**' $additionalArguments"
.\scripts\clean-typescript-main-package.ps1 -targetDirectory $targetDirectory