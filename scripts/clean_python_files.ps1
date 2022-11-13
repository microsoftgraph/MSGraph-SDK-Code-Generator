# Remove old generated files.
Remove-Item -Recurse $env:RepoModelsDir | Write-Host
Write-Host "Removed the existing generated files in the directory: $env:RepoModelsDir." -ForegroundColor Green