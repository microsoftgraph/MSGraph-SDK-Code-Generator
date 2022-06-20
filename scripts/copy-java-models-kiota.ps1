Write-Host "Path of new models: $env:OutputFullPath"
Write-Host "Path to repo models directory: $env:RepoModelsDir"
 
#copy new modes
# Create a dest folder explicitly https://github.com/PowerShell/PowerShell/issues/13352#issuecomment-669025179
New-Item -ItemType directory -Path $env:RepoModelsDir
Move-Item $env:OutputFullPath $env:RepoModelsDir -Force
Write-Host "Moved the models from $env:OutputFullPath into the local repo $env:RepoModelsDir." -ForegroundColor Green