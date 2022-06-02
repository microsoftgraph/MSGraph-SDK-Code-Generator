Remove-Item -Recurse $env:MainDirectory | Write-Host
Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green