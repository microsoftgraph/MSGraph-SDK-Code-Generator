$v1GeneratedFiles = Join-Path $env:OutputFullPathV1 "/com/Microsoft/Graph/*"

# copy generated files by preserving the subdirectory structure from /com/microsoft/graph/
# the same directory structure is expected to appear in repository's src/ directory after copy
Copy-Item $v1GeneratedFiles $env:RepoModelsDirV1 -Recurse -Force
Write-Host "Moved the models with path $v1GeneratedFiles into the local repo." -ForegroundColor Green

$betaGeneratedFiles = Join-Path $env:OutputFullPathBeta "/com/Beta/*"
New-Item -Path $env:RepoModelsDirBeta -ItemType Directory

# copy generated files by preserving the subdirectory structure from /com/Beta
# the same directory structure is expected to appear in repository's src/Beta directory after copy
Copy-Item $betaGeneratedFiles $env:RepoModelsDirBeta -Recurse -Force
Write-Host "Moved the models with path $betaGeneratedFiles into the local repo." -ForegroundColor Green