# removes all models under src/Beta
Remove-Item $env:RepoModelsDir -Recurse -Force
Write-Host "Removed the existing generated files for Beta in the repo at $($env:RepoModelsDir)." -ForegroundColor Green

$betaGeneratedFiles = Join-Path $env:OutputFullPath "/com/Beta/*"
New-Item -Path $env:RepoModelsDir -ItemType Directory

# copy generated files by preserving the subdirectory structure from /com/Beta
# the same directory structure is expected to appear in repository's src/Beta directory after copy
Copy-Item $betaGeneratedFiles $env:RepoModelsDir -Recurse -Force
Write-Host "Moved the models with path $betaGeneratedFiles into the local repo." -ForegroundColor Green