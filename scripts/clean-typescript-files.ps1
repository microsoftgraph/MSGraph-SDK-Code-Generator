
Rename-Item $env:MainDirectory "lib"
New-Item $env:MainDirectory -ItemType Directory

Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green
