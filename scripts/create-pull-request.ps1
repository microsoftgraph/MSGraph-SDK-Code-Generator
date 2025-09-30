if (($env:OverrideSkipCI -eq $False) -and ($env:BUILD_REASON -eq 'Manual')) # Skip CI if manually running this pipeline.
{
    Write-Host "Skipping pull request creation due Skip CI." -ForegroundColor Green
    return;
}

if (($env:GeneratePullRequest -eq $False)) { # Skip CI if manually running this pipeline.
    Write-Host "Skipping pull request creation due this repository being disabled" -ForegroundColor Green
    return;
}

# Special case for beta typings as it uses a non-conforming preview versioning.
if ($env:RepoName.Contains("msgraph-beta-typescript-typings"))
{
    $title = "feat: generated $version models and request builders"    
}
else {
    $title = "Generated $version models and request builders"
}

$version = $env:Version
$body = ":bangbang:**_Important_**:bangbang: <br> Check for unexpected deletions or changes in this PR and ensure relevant CI checks are passing. <br><br> **Note:** This pull request was automatically created by Azure pipelines."
$baseBranchParameter = ""

if (![string]::IsNullOrEmpty($env:BaseBranch))
{
    $baseBranchParameter = "-B $env:BaseBranch" # optionally pass the base branch if provided as the PR will target the default branch otherwise
}

# The installed application is required to have the following permissions: read/write on pull requests/
$tokenGenerationScript = "$env:ScriptsDirectory\Generate-Github-Token.ps1"
$env:GITHUB_TOKEN = & $tokenGenerationScript -AppClientId $env:GhAppId -AppPrivateKeyContents $env:GhAppKey -Repository $env:RepoName
Write-Host "Fetched Github Token for PR generation and set as environment variable." -ForegroundColor Green

# No need to specify reviewers as code owners should be added automatically.
Invoke-Expression "gh auth login" # login to GitHub
Invoke-Expression "gh pr create -t ""$title"" -b ""$body"" $baseBranchParameter | Write-Host"

Write-Host "Pull Request Created successfully." -ForegroundColor Green