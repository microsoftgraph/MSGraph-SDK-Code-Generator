param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [string]
    $packageName = "@microsoft/msgraph-sdk"
)
Push-Location $targetDirectory

$kiotaLockFileName = "kiota-lock.json"
$mainPackageDirectoryName = $packageName.Split("/")[1]
$mainPackageDirectoryPath = Join-Path $targetDirectory -ChildPath $mainPackageDirectoryName

Push-Location $mainPackageDirectoryPath
Get-ChildItem -Directory | ForEach-Object { Remove-Item -r $_.FullName }
Remove-Item $kiotaLockFileName -ErrorAction SilentlyContinue -Verbose
Pop-Location

$directories = Get-ChildItem -Directory -Exclude $mainPackageDirectoryName | Where-Object { -not($_.Name.EndsWith("-tests")) }
foreach ($directory in $directories) {
    Push-Location $directory.FullName
    Get-ChildItem -Directory | ForEach-Object {Remove-Item -r $_.FullName}
	Remove-Item *.ts -Exclude "index.ts", "*ServiceClient.ts"
    Remove-Item $kiotaLockFileName -ErrorAction SilentlyContinue -Verbose
    Pop-Location
}
Pop-Location
Write-Host "Removed the existing generated files in the repo's main directory: $targetDirectory" -ForegroundColor Green
