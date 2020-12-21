$generatedFile = Join-Path $env:OutputFullPath "/com/microsoft/graph/src/Microsoft-graph.d.ts"
$destinationFile = Join-Path $env:RepoModelsDir "microsoft-graph.d.ts"

Copy-Item $generatedFile $destinationFile -Force
Write-Host "Copied from $generatedFile to $destinationFile." -ForegroundColor Green