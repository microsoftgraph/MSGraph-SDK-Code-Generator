Write-Host "Path to repo models directory: $env:RepoModelsDir"

# clean old models
Remove-Item -Recurse $env:RepoModelsDir | Write-Host
Write-Host "Removed the existing generated files in the repo." -ForegroundColor Green

# copy new models
$modelsDirectory = Join-Path $env:OutputFullPath "/com/microsoft/graph/"
Move-Item $modelsDirectory $env:RepoModelsDir
Write-Host "Moved the models from $modelsDirectory into the local repo." -ForegroundColor Green
