if (($env:OverrideSkipCI -eq $False) -and ($env:BUILD_REASON -eq 'Manual')) # Skip CI if manually running this pipeline.
{
    Write-Host "Skipping pull request creation due Skip CI." -ForegroundColor Green
    return;
}

if (($env:GeneratePullRequest -eq $False)) { # Skip CI if manually running this pipeline.
    Write-Host "Skipping pull request creation due this repository being disabled" -ForegroundColor Green
    return;
}

$version = $env:Version
$title = "Generated $version models and request builders"
$body = "This pull request was automatically created by Azure Pipelines. **Important** Check for unexpected deletions or changes in this PR."
$baseBranchParameter = ""

if (![string]::IsNullOrEmpty($env:BaseBranch))
{
    $baseBranchParameter = "-B $env:BaseBranch" # optionally pass the base branch if provided as the PR will target the default branch otherwise
}

 # No need to specify reviewers as code owners should be added automatically.
Invoke-Expression "gh auth login" # login to GitHub
Invoke-Expression "gh pr create -t ""$title"" -b ""$body"" $baseBranchParameter | Write-Host"

Write-Host "Pull Request Created successfully." -ForegroundColor Green