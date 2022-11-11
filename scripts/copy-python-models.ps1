Write-Host "Path to repo models directory: $env:RepoModelsDir"

If(!(test-path $env:RepoModelsDir))
{
    New-Item -ItemType Directory -Force -Path $env:RepoModelsDir
}

Copy-Item $env:OutputFullPath -Destination $env:RepoModelsDir -Recurse -Force
Write-Host "Copied the generated files from: $env:OutputFullPath to: $env:RepoModelsDir" -ForegroundColor Green