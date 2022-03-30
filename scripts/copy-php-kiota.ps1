Write-Host "Path to repo models directory: $env:RepoModelsDir"

# clean old generated files.
Remove-Item -Recurse $env:RepoModelsDir | Write-Host
Write-Host "Removed the existing generated files in the repo." -ForegroundColor Green

# copy new generated files.
Move-Item $env:OutputFullPath $env:RepoModelsDir
Write-Host "Moved the models from $env:OutputFullPath into the local repo." -ForegroundColor Green
