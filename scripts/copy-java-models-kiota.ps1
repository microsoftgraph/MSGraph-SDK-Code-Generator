Write-Host "Path to repo models directory: $env:RepoModelsDir"

#copy new modes
Move-Item $env:OutputFullPath $env:RepoModelsDir
Write-Host "Moved the models from $env:OutputFullPath into the local repo." -ForegroundColor Green