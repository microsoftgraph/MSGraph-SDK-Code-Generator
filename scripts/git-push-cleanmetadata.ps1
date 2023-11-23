if ($env:PublishChanges -eq $False)
{
    Write-Host "Not publishing changes per the run parameter!" -ForegroundColor Green
    return;
}

Write-Host "About to add clean $env:EndpointVersion metadata file....."

git fetch origin is/metadata-pipeline-test
# checkout is/metadata-pipeline-test to move from detached HEAD mode
git switch is/metadata-pipeline-test

git add . | Write-Host
if ($env:BUILD_REASON -eq 'Manual') # Skip CI if manually running this pipeline.
{
    git commit -m "Update clean metadata file with $env:BUILD_BUILDID [skip ci]" | Write-Host
}
else
{
    git commit -m "Update clean metadata file with $env:BUILD_BUILDID" | Write-Host
}

Write-Host "Added and commited cleaned $env:EndpointVersion metadata." -ForegroundColor Green

# sync branch before pushing
# this is especially important while running v1 and beta in parallel
# and one process goes out of sync because of the other's check-in
git pull origin is/metadata-pipeline-test --rebase

git push --set-upstream origin is/metadata-pipeline-test | Write-Host
Write-Host "Pushed the results of the build $env:BUILD_BUILDID to the is/metadata-pipeline-test branch." -ForegroundColor Green