if (!$env:PublishChanges)
{
    Write-Host "Not publishing changes as a branch per the run parameter!" -ForegroundColor Green
    return;
}

Write-Host "About to add files....." -ForegroundColor Green
git add . | Write-Host
if ($env:BUILD_REASON -eq 'Manual') # Skip CI if manually running this pipeline.
{
    git commit -m "Update generated files with build $env:BUILD_BUILDID [skip ci]" | Write-Host
}
else
{
    git commit -m "Update generated files with build $env:BUILD_BUILDID" | Write-Host
}

Write-Host "Added and commited generated files." -ForegroundColor Green

git push --set-upstream origin $env:BranchName | Write-Host
Write-Host "Pushed the results of the build to the $env:BranchName branch." -ForegroundColor Green