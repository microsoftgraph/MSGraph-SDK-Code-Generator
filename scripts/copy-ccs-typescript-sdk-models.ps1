param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [Parameter(Mandatory = $true)]
    [string]
    $sourceDirectory,
    [Parameter(Mandatory = $true)]
    [string]
    $packageName
)

Write-Host "Path to repo models directory: $targetDirectory"
$mainPackageDirectoryName = $packageName.Split("/")[1]
$modelsPackagePath = Join-Path $targetDirectory -ChildPath $mainPackageDirectoryName -AdditionalChildPath "generated"
Copy-Item $sourceDirectory -Destination $modelsPackagePath -Recurse -Force
