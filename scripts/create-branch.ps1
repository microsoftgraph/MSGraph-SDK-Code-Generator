Push-Location $env:RepoDirectory
git checkout -b $env:BranchName
Write-Host "Created branch: $env:BranchName in $env:RepoDirectory"
Pop-Location