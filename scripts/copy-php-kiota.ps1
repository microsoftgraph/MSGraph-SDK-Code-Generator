Write-Host "Path to repo models directory: $env:RepoModelsDir"

# clean old models
Remove-Item -Recurse $env:RepoModelsDir | Write-Host
Write-Host "Removed the existing generated files in the repo." -ForegroundColor Green

# copy new models
Move-Item $env:OutputFullPath $env:RepoModelsDir
Write-Host "Moved the models from $env:OutputFullPath into the local repo." -ForegroundColor Green
