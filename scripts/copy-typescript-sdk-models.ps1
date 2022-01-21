Write-Host "Path to repo models directory: $env:RepoModelsDir"

Copy-Item $env:OutputFullPath -Destination $env:RepoModelsDir -Recurse -Force
Write-Host "Copied the generated files into the repo. From: $env:OutputFullPath to: $env:RepoModelsDir" -ForegroundColor Green
