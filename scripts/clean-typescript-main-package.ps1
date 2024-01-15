param (
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory
)
if ($targetDirectory -eq $null) {
    Write-Error "Target directory is required"
    Exit 1
}
Push-Location $targetDirectory
Remove-Item *.ts -Verbose -Exclude "index.ts"
Get-ChildItem -Directory -Exclude models | ForEach-Object {Remove-Item -r $_.FullName}
Pop-Location
