param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [Parameter(Mandatory = $true)]
    [string]
    $sourceDirectory,
    [string]
    $packageName = "@microsoft/msgraph-sdk"
)

Write-Host "Path to repo models directory: $targetDirectory"
$mainPackageDirectoryName = $packageName.Split("/")[1]
$modelsPackagePath = Join-Path $targetDirectory -ChildPath $mainPackageDirectoryName
Copy-Item (Join-Path $sourceDirectory -ChildPath "models") -Destination $modelsPackagePath -Recurse -Force
Invoke-Expression "$PSScriptRoot\remove-typescript-fluent-api-from-main-package.ps1 -targetDirectory $modelsPackagePath"

$packagesDirectories = Get-ChildItem $targetDirectory -Directory -Exclude $mainPackageDirectoryName | Where-Object { -not($_.Name.EndsWith("-tests")) }
foreach ($directory in $packagesDirectories) {
    $fluentAPISegmentName = $directory.Name.Replace("$mainPackageDirectoryName-", "")
    Copy-Item (Join-Path $sourceDirectory -ChildPath $fluentAPISegmentName) -Destination $directory.FullName -Recurse -Force
    Invoke-Expression "$PSScriptRoot\fix-typescript-fluent-packages-imports.ps1 -targetDirectory $($directory.FullName) -packageName $packageName"
}

Write-Host "Copied the generated files into the repo. From: $sourceDirectory to: $targetDirectory" -ForegroundColor Green
