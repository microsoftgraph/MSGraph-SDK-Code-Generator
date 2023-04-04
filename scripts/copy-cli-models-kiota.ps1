Write-Host "Path to repo models directory: $env:RepoModelsDir"

# clean old models
Remove-Item -Recurse $env:RepoModelsDir | Write-Host
Write-Host "Removed the existing generated files in the repo." -ForegroundColor Green

# copy new models
# Create a dest folder explicitly https://github.com/PowerShell/PowerShell/issues/13352#issuecomment-669025179
New-Item -ItemType directory -Path $env:RepoModelsDir
Move-Item $env:OutputFullPath $env:RepoModelsDir
Write-Host "Moved the models from $modelsDirectory into the local repo." -ForegroundColor Green
