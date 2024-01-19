param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [Parameter(Mandatory = $true)]
    [string]
    $sourceDirectory,
    [string]
    $packageName = "@microsoft/msgraph-sdk-javascript"
)
if ($targetDirectory -eq $null) {
    Write-Error "Target directory is required"
    Exit 1
}

Write-Host "Path to repo models directory: $targetDirectory"
$mainPackageDirectoryName = $packageName.Split("/")[1]
$modelsPackagePath = Join-Path $targetDirectory -ChildPath $mainPackageDirectoryName
Copy-Item (Join-Path $sourceDirectory -ChildPath "models") -Destination $modelsPackagePath -Recurse -Force
.\scripts\remove-typescript-fluent-api-from-main-package.ps1 -targetDirectory $modelsPackagePath

$packagesDirectories = Get-ChildItem $targetDirectory -Directory -Exclude $mainPackageDirectoryName
foreach ($directory in $packagesDirectories) {
    $packageName = $directory.Name.Replace("$mainPackageDirectoryName-", "")
    Copy-Item (Join-Path $sourceDirectory -ChildPath $packageName) -Destination $directory.FullName -Recurse -Force
    .\scripts\fix-typescript-fluent-packages-imports.ps1 -targetDirectory $directory.FullName -packageName $packageName
}

Write-Host "Copied the generated files into the repo. From: $env:OutputFullPath to: $env:RepoModelsDir" -ForegroundColor Green
