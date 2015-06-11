## REPO_ROOT\scripts\appveyor\test.ps1

$REPO_ROOT = Resolve-Path -Path "${Script:PSScriptRoot}\..\.."
Write-Host ("Repo root: {0}" -f $REPO_ROOT) -ForegroundColor Yellow

## Execute integration test
Set-Location "${REPO_ROOT}\test\T4TemplateWriterTests\bin\Debug"
& .\T4TemplateWriterTests.exe

Set-Location $REPO_ROOT
