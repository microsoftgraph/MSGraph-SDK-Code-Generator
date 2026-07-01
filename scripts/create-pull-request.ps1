if (($env:OverrideSkipCI -eq $False) -and ($env:BUILD_REASON -eq 'Manual')) # Skip CI if manually running this pipeline.
{
    Write-Host "Skipping pull request creation due Skip CI."
    return;
}

if (($env:GeneratePullRequest -eq $False)) { # Skip CI if manually running this pipeline.
    Write-Host "Skipping pull request creation due this repository being disabled"
    return;
}

# Special case for beta typings as it uses a non-conforming preview versioning. Helps with triggering Release Please.
if ($env:RepoName.Contains("msgraph-beta-typescript-typings"))
{
    $title = "feat: generated $env:Version models and request builders"    
}
elseif ($env:RepoName.Contains("msgraph-metadata")) # we are only generating OpenAPI PRs for the metadata repo
{
    $title = "Generated $env:Version OpenAPI descriptions"
}
else {
    $title = "Generated $env:Version models and request builders"
}

$body = ":bangbang:**_Important_**:bangbang: <br> Check for unexpected deletions or changes in this PR and ensure relevant CI checks are passing. <br><br> **Note:** This pull request was automatically created by Azure pipelines."
$baseBranchParameter = ""

if (![string]::IsNullOrEmpty($env:BaseBranch))
{
    $baseBranchParameter = "-B $env:BaseBranch" # optionally pass the base branch if provided as the PR will target the default branch otherwise
}

$headBranchParameter = ""

if (![string]::IsNullOrEmpty($env:BranchName))
{
    # Explicitly set the head branch. On the GitHub App auth path the repository is cloned with
    # 'git clone --depth 1', which implies --single-branch and only tracks the base branch. As a
    # result no 'refs/remotes/origin/<branch>' tracking ref exists after pushing, so 'gh pr create'
    # cannot auto-detect that the branch was pushed and aborts with "you must first push the current
    # branch to a remote, or use the --head flag". Passing --head bypasses that detection.
    $headBranchParameter = "-H $env:BranchName"
}

# The installed application is required to have the following permissions: read/write on pull requests/
$tokenGenerationScript = "$env:ScriptsDirectory\Generate-Github-Token.ps1"
$env:GITHUB_TOKEN = & $tokenGenerationScript -AppClientId $env:GhAppId -AppPrivateKeyContents $env:GhAppKey -Repository $env:RepoName
Write-Host "Fetched Github Token for PR generation and set as environment variable."

# No need to specify reviewers as code owners should be added automatically.
Invoke-Expression "gh auth login" # login to GitHub
Invoke-Expression "gh pr create -R ""$env:RepoName"" -t ""$title"" -b ""$body"" $baseBranchParameter $headBranchParameter | Write-Host"

if ($LASTEXITCODE -ne 0)
{
    throw "Failed to create pull request for $env:RepoName (branch '$env:BranchName'). 'gh pr create' exited with code $LASTEXITCODE."
}

Write-Host "Pull Request Created successfully."