# Example directories are:
# src/Model (types in microsoft.graph namespace)
# src/CallRecords/Model (types in microsoft.graph.callRecords namespace)
Get-ChildItem $env:RepoModelsDir -Exclude Beta,Core,Exception,Http -Directory | Remove-Item -Force -Recurse
Write-Host "Removed the existing generated files in the repo at $modelDir." -ForegroundColor Green

$v1GeneratedFiles = Join-Path $env:OutputFullPath "/com/Microsoft/Graph/*"

# copy generated files by preserving the subdirectory structure from /com/microsoft/graph/
# the same directory structure is expected to appear in repository's src/ directory after copy
Copy-Item $v1GeneratedFiles $env:RepoModelsDir -Recurse -Force
Write-Host "Moved the models with path $v1GeneratedFiles into the local repo." -ForegroundColor Green
