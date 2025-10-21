# git-push-cleanmetadata.ps1
# Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the 
# project root for license information.

# Referenced by:
# capture-metadata.yml
# capture-openapi.yml

# This script stashes any changes, checks out the latest master branch, applies the stashed changes, commits, and 
# pushes the changes back to the remote repository.

if ($env:PublishChanges -eq $False)
{
    Write-Host "Not publishing changes per the run parameter!"
    return;
}

Write-Host "`ngit status:"
git status | Write-Host

Write-Host "`nStash the update metadata files.....`nRunning: git stash"
git stash | Write-Host

Write-Host "`nFetching latest master branch to ensure we are up to date..."
git fetch origin master | Write-Host
# checkout master to move from detached HEAD mode
git switch master | Write-Host

Write-Host "`ngit status:"
git status | Write-Host

Write-Host "`nApply stashed metadata files...`nRunning: git stash pop"
git stash pop | Write-Host

Write-Host "`ngit status:"
git status | Write-Host

if ($env:CreateOpenAPIPR -eq $True)
{
    Write-Host "`nCreate branch: $env:BUILD_BUILDID/updateOpenAPI"
    git checkout -B $env:BUILD_BUILDID/updateOpenAPI | Write-Host
}

Write-Host "`nStaging clean $env:EndpointVersion metadata files.....`nRunning: git add ."
git add . | Write-Host

Write-Host "`ngit status:"
git status | Write-Host

Write-Host "`nAttempting to commit clean $env:EndpointVersion metadata files....."

if ($env:BUILD_REASON -eq 'Manual') # Skip CI if manually running this pipeline.
{
    git commit -m "Update clean metadata file with $env:BUILD_BUILDID [skip ci]" | Write-Host
}
else
{
    git commit -m "Update clean metadata file with $env:BUILD_BUILDID" | Write-Host
}

Write-Host "`ngit status:"
git status | Write-Host

if ($env:CreateOpenAPIPR -eq $True)
{
    Write-Host "`nPushing branch for PR creation"

    Write-Host "`ngit push --set-upstream origin $env:BUILD_BUILDID/updateOpenAPI:"
    git push --set-upstream origin $env:BUILD_BUILDID/updateOpenAPI | Write-Host
}
else # original behavior: push to master
{
    Write-Host "`nRunning: git pull origin master --rebase..."
    # sync branch before pushing
    # this is especially important while running v1 and beta in parallel
    # and one process goes out of sync because of the other's check-in
    git pull origin master --rebase | Write-Host

    Write-Host "`nRunning: git push --set-upstream origin master ..."
    git push --set-upstream origin master | Write-Host
}