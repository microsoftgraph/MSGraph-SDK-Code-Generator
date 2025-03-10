param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [Parameter(Mandatory = $true)]
    [string]
    $sourceDirectory,
    [string]
    $packageName = "@microsoft/copilot-beta-sdk"
)

Write-Host "Path to repo models directory: $targetDirectory"
$mainPackageDirectoryName = $packageName.Split("/")[1]
$modelsPackagePath = Join-Path $targetDirectory -ChildPath $mainPackageDirectoryName
Copy-Item $sourceDirectory -Destination $modelsPackagePath -Recurse -Force
