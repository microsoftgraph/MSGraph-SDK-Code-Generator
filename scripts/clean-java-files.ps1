$modelsAndRequestsDirectories = Get-ChildItem $env:MainDirectory -Include models,requests -Recurse -Directory

foreach ($directory in $modelsAndRequestsDirectories)
{
    Remove-Item $directory.FullName -Recurse -Force
}
Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green
