param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [string]
    $packageName = "@microsoft/msgraph-sdk-javascript"
)
if ($targetDirectory -eq $null) {
    Write-Error "Target directory is required"
    Exit 1
}
Push-Location $targetDirectory
if (Test-Path .\models) {
    Remove-Item -r .\models\
}
$fluentAPIFiles = Get-ChildItem -File -Filter *.ts -Recurse
foreach($fluentAPIFile in $fluentAPIFiles) {
    $content = Get-Content $fluentAPIFile -raw
    $content = $content -replace "from '[^']*/models/([^;]*)';", "from '$packageName/src/models/`$1';"
    Set-Content $fluentAPIFile -Value $content
}
Pop-Location