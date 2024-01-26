param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory
)
Push-Location $targetDirectory
Remove-Item *.ts -Verbose -Exclude "index.ts", "graphServiceClient.ts", "graphBetaServiceClient.ts"
Get-ChildItem -Directory -Exclude models | ForEach-Object {Remove-Item -r $_.FullName}
Pop-Location
