Push-Location $env:RepoDirectory
git fetch
git switch $env:BranchName
Write-Host "Switching to remote branch: $env:BranchName in $env:RepoDirectory"
Pop-Location