$modelsAndRequestsDirectories = Get-ChildItem $env:MainDirectory -Include models,requests -Recurse -Directory

# this list should be updated if a new hand-crafted extension is added to one of the directories
$filesThatShouldNotBeDeleted = @()
foreach ($directory in $modelsAndRequestsDirectories)
{
    Remove-Item $directory.FullName -Recurse -Force -Exclude $filesThatShouldNotBeDeleted
}
Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green