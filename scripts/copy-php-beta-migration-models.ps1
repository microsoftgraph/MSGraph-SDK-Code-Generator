# Removes all models under src/ excluding all .php files in the root directory
# including GraphConstants.php

Get-ChildItem $env:RepoModelsDir -Exclude *.php | Remove-Item -Force -Recurse
Write-Host "Removed the existing generated files for Beta in the repo at $($env:RepoModelsDir)." -ForegroundColor Green

$betaGeneratedFiles = Join-Path $env:OutputFullPath "/com/Beta/Microsoft/Graph/*"
New-Item -Path $env:RepoModelsDir -ItemType Directory

# copy generated files by preserving the subdirectory structure from /com/Beta
# the same directory structure is expected to appear in repository's src/Beta directory after copy
Copy-Item $betaGeneratedFiles $env:RepoModelsDir -Recurse -Force
Write-Host "Moved the models with path $betaGeneratedFiles into the local repo." -ForegroundColor Green