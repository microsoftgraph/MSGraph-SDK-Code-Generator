param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [string]
    $packageName = "@microsoft/agents-m365copilot"
)
Push-Location $targetDirectory

$kiotaLockFileName = "kiota-lock.json"
# Extract folder name from package name (the part after the slash)
$folderToClean = $packageName.Split("/")[1]
$folderToCleanPath = Join-Path $targetDirectory -ChildPath $folderToClean

# Check if the folder to clean exists
if (Test-Path $folderToCleanPath) {
    Write-Host "Cleaning contents in: $folderToClean" -ForegroundColor Cyan
    Push-Location $folderToCleanPath
    # Clean directories
    Get-ChildItem -Directory | ForEach-Object { Remove-Item -r $_.FullName }
    # Clean TypeScript files except index.ts and *ServiceClient.ts
    Remove-Item *.ts -Exclude "index.ts", "*ServiceClient.ts"
    # Remove kiota lock file
    Remove-Item $kiotaLockFileName -ErrorAction SilentlyContinue -Verbose
    Pop-Location
    Write-Host "Successfully cleaned contents in: $folderToClean" -ForegroundColor Green
} else {
    Write-Host "Warning: Folder '$folderToClean' does not exist in the target directory." -ForegroundColor Yellow
}

Pop-Location
Write-Host "Cleaning operation completed for target: $folderToClean derived from package: $packageName" -ForegroundColor Green