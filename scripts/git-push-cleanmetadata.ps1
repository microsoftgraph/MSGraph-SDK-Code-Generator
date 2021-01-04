Write-Host "About to add clean $env:endpointVersion metadata file....."

git add . | Write-Host
if ($env:BUILD_REASON -eq 'Manual') # Skip CI if manually running this pipeline.
{
    git commit -m "Update clean metadata file with $env:BUILD_BUILDID [skip ci]" | Write-Host
}
else
{
    git commit -m "Update clean metadata file with $env:BUILD_BUILDID" | Write-Host
}

Write-Host "Added and commited cleaned $env:endpointVersion metadata." -ForegroundColor Green

git push --set-upstream origin master | Write-Host
Write-Host "Pushed the results of the build $env:BUILD_BUILDID to the master branch." -ForegroundColor Green