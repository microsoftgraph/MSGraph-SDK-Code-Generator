## REPO_ROOT\scripts\appveyor\before_build.ps1

$REPO_ROOT = Resolve-Path -Path "${Script:PSScriptRoot}\..\.."
Write-Host ("REPO_ROOT: {0}" -f $REPO_ROOT) -ForegroundColor Yellow

$ASM_INFO_FILE      = '{0}\submodules\vipr\src\Core\Vipr\Properties\AssemblyInfo.cs' -f $REPO_ROOT
$VIPR_CSPROJ        = '{0}\submodules\vipr\src\Core\Vipr\Vipr.csproj' -f $REPO_ROOT
$ASM_TEST           = 'T4TemplateWriterTests'
$INSTRUCTION_TO_ADD = '[assembly: InternalsVisibleTo("{0}")]' -f $ASM_TEST

$MSBUILD_VERSION    = '12.0'
$MSBUILD            = Join-Path -Path ("${env:ProgramFiles(x86)}\MSBuild\{0}\Bin" -f $MSBUILD_VERSION) -ChildPath 'MSBuild.exe'

Write-Host "Cloning submodules" -ForegroundColor Yellow
& git submodule update --init --recursive
Write-Host ("Error Details: {0}" -f $Error[0].Exception.InnerException) -ForegroundColor Yellow

## Enable InternalsVisibleTo("T4TemplateWriterTests") for testing
$InstructionAdded = Select-String -Path $ASM_INFO_FILE -Pattern ('\[assembly: InternalsVisibleTo\("{0}"\)\]' -f $ASM_TEST) -Quiet
if (-not $InstructionAdded) {
    Add-Content -Path $ASM_INFO_FILE -Value $INSTRUCTION_TO_ADD
    Write-Host -Object "Instruction added to Core\Vipr\Properties\AssemblyInfo.cs." -ForegroundColor Yellow
} else {
    Write-Host -Object "Instruction already present in Core\Vipr\Properties\AssemblyInfo.cs." -ForegroundColor Yellow
}

## Remove 'Vipr.Writer.CSharp' reference from .csproj
$ProjectXml = [System.Xml.XmlDocument] (Get-Content -Path $VIPR_CSPROJ)
if ($InvalidNode = $ProjectXml.Project.ItemGroup.ProjectReference | ? {$_.Name -eq 'Vipr.Writer.CSharp'}) {
    ($ProjectXml.Project.ItemGroup | ? {$_.ProjectReference}).RemoveChild($InvalidNode) | Out-Null
    $ProjectXml.Save($VIPR_CSPROJ)
    Write-Host -Object "Removed CSharp writer project from references." -ForegroundColor Yellow
} else {
    Write-Host -Object "CSharp writer project not found in references, not removed." -ForegroundColor Yellow
}

## Restore nuget packages *within* submodule
## This is needed so relative paths in CSPROJ files work
Write-Host -Object "`nRestoring Nuget packages within Vipr" -ForegroundColor Yellow
Set-Location -Path "${REPO_ROOT}\submodules\vipr"
Write-Host -Object ('PWD: {0}' -f $Variable:PWD) -ForegroundColor Yellow
& nuget.exe restore

Write-Host -Object "`nRestoring Nuget packages for T4 Template Writer" -ForegroundColor Yellow
Set-Location -Path $REPO_ROOT
Write-Host -Object ('PWD: {0}' -f $Variable:PWD) -ForegroundColor Yellow
& nuget.exe restore

Set-Location $REPO_ROOT
