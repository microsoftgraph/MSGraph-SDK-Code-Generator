Write-Host "Path of new models: $env:OutputFullPath"
Write-Host "Path to repo models directory: $env:RepoModelsDir"
 
#copy new modes
Move-Item $env:OutputFullPath $env:RepoModelsDir -Force
Write-Host "Moved the models from $env:OutputFullPath into the local repo $env:RepoModelsDir." -ForegroundColor Green