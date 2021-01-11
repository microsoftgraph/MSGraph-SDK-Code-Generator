Write-Host "Path to repo models directory: $env:RepoModelsDir"

$comDirectory = Join-Path $env:OutputFullPath "com/"

# Path to the destination directory
$comDestinationDirectory = Join-Path $env:RepoModelsDir "/src/main/java/"

Copy-Item $comDirectory -Destination $comDestinationDirectory -Recurse -Force
Write-Host "Copied the generated com/ files into the repo. From: $comDirectory to: $comDestinationDirectory" -ForegroundColor Green
