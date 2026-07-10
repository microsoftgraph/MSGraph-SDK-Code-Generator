# Copyright (c) Microsoft Corporation. All rights reserved.
# Licensed under the MIT License.

# Configure the 'origin' push URL of an already-cloned repository to authenticate with a
# GitHub App installation token instead of a PAT. This lets a repo be pushed to using the
# App identity (the same one that opens the PRs), which removes the need for a dedicated
# PAT when the repo isn't covered by the pipeline's shared service connection.
#
# Only the push URL is changed; the fetch URL is left untouched (repos are public hence reads  
# can be maintained as anonymous). 
# Required environment variables:
#   GhAppId          - GitHub App client ID (from AKV)
#   GhAppKey         - GitHub App private key (from AKV)
#   RepoName         - Target repo in "owner/repo" format (e.g. "microsoft/Agents-M365Copilot")
#   ScriptsDirectory - Path to the scripts folder containing Generate-Github-Token.ps1
# Runs from the repository working directory (the cloned repo).

[CmdletBinding()]
param ()

$ErrorActionPreference = "Stop"

if ([string]::IsNullOrWhiteSpace($env:GhAppId))         { throw "GhAppId is required" }
if ([string]::IsNullOrWhiteSpace($env:GhAppKey))        { throw "GhAppKey is required" }
if ([string]::IsNullOrWhiteSpace($env:RepoName))        { throw "RepoName is required" }
if ([string]::IsNullOrWhiteSpace($env:ScriptsDirectory)){ throw "ScriptsDirectory is required" }

# The installed application is required to have contents:write on the target repository.
$tokenGenerationScript = Join-Path $env:ScriptsDirectory "Generate-Github-Token.ps1"
$token = & $tokenGenerationScript -AppClientId $env:GhAppId -AppPrivateKeyContents $env:GhAppKey -Repository $env:RepoName
if ([string]::IsNullOrWhiteSpace($token)) {
    throw "Failed to generate GitHub App installation token (empty result)"
}

# Mask the token so it is never surfaced in pipeline logs.
Write-Host "##vso[task.setsecret]$token"

# Set the push URL only; leave the (anonymous) fetch URL alone.
$pushUrl = "https://x-access-token:$token@github.com/$($env:RepoName).git"
git remote set-url --push origin $pushUrl
if ($LASTEXITCODE -ne 0) { throw "Failed to set push URL for origin" }

Write-Host "Configured origin push URL to use the GitHub App installation token." -ForegroundColor Green
