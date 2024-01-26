param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [string]
    $packageName = "@microsoft/msgraph-sdk"
)
Push-Location $targetDirectory
if (Test-Path .\models) {
    Remove-Item -r .\models\
}
$fluentAPIFiles = Get-ChildItem -File -Filter *.ts -Recurse
foreach($fluentAPIFile in $fluentAPIFiles) {
    $content = Get-Content $fluentAPIFile -raw
    $content = $content -replace "from '[^']*/models/([^;]*)';", "from '$packageName/models/`$1';"
    Set-Content $fluentAPIFile -Value $content -NoNewline
}
Pop-Location