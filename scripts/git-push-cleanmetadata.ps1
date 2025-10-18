# Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the 
# project root for license information.

# This script stashes any changes, checks out the latest master branch, applies the stashed changes, commits, and 
# pushes the changes back to the remote repository.

if ($env:PublishChanges -eq $False)
{
    Write-Host "Not publishing changes per the run parameter!" -ForegroundColor Red
    return;
}

Write-Host "`nGet status:" -ForegroundColor Green
git status | Write-Host -ForegroundColor Yellow

Write-Host "`nStash the update metadata files.....`nRunning: git stash" -ForegroundColor Green
git stash | Write-Host -ForegroundColor Yellow

Write-Host "`nFetching latest master branch to ensure we are up to date..." -ForegroundColor Green
git fetch origin master | Write-Host -ForegroundColor Yellow
# checkout master to move from detached HEAD mode
git switch master | Write-Host -ForegroundColor Yellow

Write-Host "`nGet status:" -ForegroundColor Green
git status | Write-Host -ForegroundColor Yellow

Write-Host "`nApply stashed metadata files...`nRunning: git stash pop" -ForegroundColor Green
git stash pop | Write-Host -ForegroundColor Yellow

Write-Host "`nGet status:" -ForegroundColor Green
git status | Write-Host -ForegroundColor Yellow

Write-Host "`nStaging clean $env:EndpointVersion metadata files....." -ForegroundColor Green
git add . | Write-Host -ForegroundColor Yellow

Write-Host "`nGet status:" -ForegroundColor Green
git status | Write-Host -ForegroundColor Yellow

Write-Host "`nAttempting to commit clean $env:EndpointVersion metadata files....." -ForegroundColor Green

if ($env:BUILD_REASON -eq 'Manual') # Skip CI if manually running this pipeline.
{
    git commit -m "Update clean metadata file with $env:BUILD_BUILDID [skip ci]" | Write-Host -ForegroundColor Yellow
}
else
{
    git commit -m "Update clean metadata file with $env:BUILD_BUILDID" | Write-Host -ForegroundColor Yellow
}

Write-Host "`nGet status:" -ForegroundColor Green
git status | Write-Host -ForegroundColor Yellow

Write-Host "`nRunning: git pull origin master --rebase..." -ForegroundColor Green
# sync branch before pushing
# this is especially important while running v1 and beta in parallel
# and one process goes out of sync because of the other's check-in
git pull origin master --rebase | Write-Host -ForegroundColor Yellow

Write-Host "`nRunning: git push --set-upstream origin master ..." -ForegroundColor Green
git push --set-upstream origin master | Write-Host -ForegroundColor Yellow