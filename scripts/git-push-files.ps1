if ($env:PublishChanges -eq $False)
{
    Write-Host "Not publishing changes as a branch per the run parameter!" -ForegroundColor Green
    return;
}

Write-Host "About to add files....." -ForegroundColor Green

if ($env:OverrideSkipCI -eq $True)
{
    Write-Host "Overriding [skip ci] flag.." -ForegroundColor Yellow
}

git add . | Write-Host
commitMessage = "${env:CommitMessagePrefix}Update generated files with build $env:BUILD_BUILDID"
if (($env:OverrideSkipCI -eq $False) -and ($env:BUILD_REASON -eq 'Manual')) # Skip CI if manually running this pipeline.
{
    git commit -m "${commitMessage} [skip ci]" | Write-Host
}
else
{
    git commit -m $commitMessage | Write-Host
}

Write-Host "Added and commited generated files." -ForegroundColor Green

git push --set-upstream origin $env:BranchName | Write-Host
Write-Host "Pushed the results of the build to the $env:BranchName branch." -ForegroundColor Green