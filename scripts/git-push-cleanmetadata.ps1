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

# Cloud outputs may be new directories. Stash and stage only their generated files.
if (![string]::IsNullOrWhiteSpace($env:PublishPath))
{
    function Invoke-PublicationGit {
        param([string[]]$Arguments)
        & git @Arguments | Write-Host
        if ($LASTEXITCODE -ne 0) {
            throw "Git publication command '$($Arguments -join ' ')' failed with exit code $LASTEXITCODE."
        }
    }

    if ([string]::IsNullOrWhiteSpace($env:PublicationIdentifier)) {
        throw "PublicationIdentifier is required when PublishPath is specified."
    }

    $changes = & git status --porcelain -- $env:PublishPath
    if ($LASTEXITCODE -ne 0) {
        throw "Failed to inspect generated files for publication."
    }
    if (!$changes) {
        Write-Host "No changes to publish in $env:PublishPath."
        Write-Host "##vso[task.setvariable variable=publicationHasChanges]false"
        return
    }

    Invoke-PublicationGit -Arguments @('stash', 'push', '--include-untracked', '--', $env:PublishPath)
    Invoke-PublicationGit -Arguments @('fetch', 'origin', 'master')
    Invoke-PublicationGit -Arguments @('switch', 'master')
    Invoke-PublicationGit -Arguments @('stash', 'pop')

    $branch = "publish-open-api-files/$env:BUILD_BUILDID/$env:PublicationIdentifier"
    if ($env:CreateOpenAPIPR -eq $True) {
        Invoke-PublicationGit -Arguments @('checkout', '-B', $branch)
    }

    Invoke-PublicationGit -Arguments @('add', '--', $env:PublishPath)
    $message = "Update clean $env:PublicationIdentifier metadata file with $env:BUILD_BUILDID"
    if ($env:BUILD_REASON -eq 'Manual') {
        $message += " [skip ci]"
    }
    Invoke-PublicationGit -Arguments @('commit', '-m', $message)
    if ($env:CreateOpenAPIPR -eq $True) {
        Invoke-PublicationGit -Arguments @('push', '--set-upstream', 'origin', $branch)
    }
    else {
        Invoke-PublicationGit -Arguments @('pull', 'origin', 'master', '--rebase')
        Invoke-PublicationGit -Arguments @('push', '--set-upstream', 'origin', 'master')
    }
    Write-Host "##vso[task.setvariable variable=publicationHasChanges]true"
    return
}

Write-Host "`n1. git status:"
git status | Write-Host

Write-Host "`n2. Stash the update metadata files.....`n3. Running: git stash"
git stash | Write-Host

Write-Host "`n4. Fetching latest master branch to ensure we are up to date..."
git fetch origin master | Write-Host
# checkout master to move from detached HEAD mode
git switch master | Write-Host



Write-Host "`n5. git status:"
git status | Write-Host

Write-Host "`n6. Apply stashed metadata files...`n7. Running: git stash pop"
git stash pop | Write-Host

Write-Host "`n8. git status:"
git status | Write-Host

$branch = "publish-open-api-files/$env:BUILD_BUILDID/$env:EndpointVersion"

if ($env:CreateOpenAPIPR -eq $True)
{
    Write-Host "`n9. Create branch: $branch"
    git checkout -B $branch | Write-Host
}

Write-Host "`n10. Staging clean $env:EndpointVersion metadata files.....`n11. Running: git add ."
git add . | Write-Host

Write-Host "`n12. git status:"
git status | Write-Host

Write-Host "`n13. Attempting to commit clean $env:EndpointVersion metadata files....."

if ($env:BUILD_REASON -eq 'Manual') # Skip CI if manually running this pipeline.
{
    git commit -m "Update clean $env:EndpointVersion metadata file with $env:BUILD_BUILDID [skip ci]" | Write-Host
}
else
{
    git commit -m "Update clean $env:EndpointVersion metadata file with $env:BUILD_BUILDID" | Write-Host
}

Write-Host "`n14. git status:"
git status | Write-Host

if ($env:CreateOpenAPIPR -eq $True)
{
    Write-Host "`n15a. Pushing branch for PR creation"

    Write-Host "`n15b. Running: git push --set-upstream origin $branch"
    git push --set-upstream origin $branch | Write-Host
}
else # original behavior: push to master
{
    Write-Host "`n15c. Running: git pull origin master --rebase..."
    # sync branch before pushing
    # this is especially important while running v1 and beta in parallel
    # and one process goes out of sync because of the other's check-in
    git pull origin master --rebase | Write-Host

    Write-Host "`n15d. Running: git push --set-upstream origin master ..."
    git push --set-upstream origin master | Write-Host
}