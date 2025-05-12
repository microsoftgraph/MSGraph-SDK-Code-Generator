param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [string]
    $packageName = "@microsoft/agents-m365copilot",
    [Parameter(Mandatory = $true)]
    [string]
    $folderToClean
)
Push-Location $targetDirectory

$kiotaLockFileName = "kiota-lock.json"
$folderToCleanPath = Join-Path $targetDirectory -ChildPath $folderToClean

# Check if the folder to clean exists
if (Test-Path $folderToCleanPath) {
    Write-Host "Cleaning contents in: $folderToClean" -ForegroundColor Cyan
    Push-Location $folderToCleanPath
    Get-ChildItem -Directory | ForEach-Object { Remove-Item -r $_.FullName }
    Remove-Item $kiotaLockFileName -ErrorAction SilentlyContinue -Verbose
    Pop-Location
    Write-Host "Successfully cleaned contents in: $folderToClean" -ForegroundColor Green
} else {
    Write-Host "Warning: Folder '$folderToClean' does not exist in the target directory." -ForegroundColor Yellow
}

Pop-Location
Write-Host "Cleaning operation completed for target: $folderToClean" -ForegroundColor Green