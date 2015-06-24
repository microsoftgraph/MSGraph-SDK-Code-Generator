## REPO_ROOT\scripts\appveyor\build.ps1

$REPO_ROOT = Resolve-Path -Path "${Script:PSScriptRoot}\..\.."
Write-Host ("REPO_ROOT: {0}" -f $REPO_ROOT) -ForegroundColor Yellow

$MSBUILD_VERSION    = '12.0'
$MSBUILD            = Join-Path -Path ("${env:ProgramFiles(x86)}\MSBuild\{0}\Bin" -f $MSBUILD_VERSION) -ChildPath 'MSBuild.exe'

## Build test library, which builds everything else transitively
& $MSBUILD vipr-t4templatewriter.sln /target:test\T4TemplateWriterTests:Rebuild
