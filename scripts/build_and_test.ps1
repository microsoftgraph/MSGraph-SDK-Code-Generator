$REPO_ROOT = Resolve-Path -Path "${Script:PSCommandPath}\..\.."
Write-Host ("Repo root: {0}" -f $REPO_ROOT)

$ASM_INFO_FILE      = '{0}\submodules\vipr\src\Core\Vipr\Properties\AssemblyInfo.cs' -f $REPO_ROOT
$VIPR_CSPROJ        = '{0}\submodules\vipr\src\Core\Vipr\Vipr.csproj' -f $REPO_ROOT
$ASM_TEST           = 'T4TemplateWriterTests'
$INSTRUCTION_TO_ADD = '[assembly: InternalsVisibleTo("{0}")]' -f $ASM_TEST
$MSBUILD_VERSION    = '12.0'
$MSBUILD            = Join-Path -Path ("${env:ProgramFiles(x86)}\MSBuild\{0}\Bin" -f $MSBUILD_VERSION) -ChildPath 'MSBuild.exe'

## Enable InternalsVisibleTo("T4TemplateWriterTests") for testing
$InstructionAdded = Select-String -Path $ASM_INFO_FILE -Pattern ('\[assembly: InternalsVisibleTo\("{0}"\)\]' -f $ASM_TEST) -Quiet
if (-not $InstructionAdded) {
    Add-Content -Path $ASM_INFO_FILE -Value $INSTRUCTION_TO_ADD
    Write-Host -Object "Instruction added to Core\Vipr\Properties\AssemblyInfo.cs."
} else {
    Write-Host -Object "Instruction already present in Core\Vipr\Properties\AssemblyInfo.cs."
}

## Remove 'Vipr.Writer.CSharp' reference from .csproj
$ProjectXml = [System.Xml.XmlDocument] (Get-Content -Path $VIPR_CSPROJ)
if ($InvalidNode = $ProjectXml.Project.ItemGroup.ProjectReference | ? {$_.Name -eq 'Vipr.Writer.CSharp'}) {
    ($ProjectXml.Project.ItemGroup | ? {$_.ProjectReference}).RemoveChild($InvalidNode)
    $ProjectXml.Save($VIPR_CSPROJ)
    Write-Host -Object "Removed CSharp writer project from references."
} else {
    Write-Host -Object "CSharp writer project not found in references, not removed."
}

## Download latest version of nuget.exe
$client = New-Object -TypeName System.Net.WebClient
$client.DownloadFile("https://www.nuget.org/nuget.exe", "${env:TEMP}\nuget.exe");

## Restore nuget packages *within* submodule
## This is needed so relative paths in CSPROJ files work
Write-Host -Object "`nRestoring Nuget packages within Vipr"
Set-Location -Path "${REPO_ROOT}\submodules\vipr"
& ${env:TEMP}\nuget.exe restore

Write-Host -Object "`nRestoring Nuget packages for T4 Template Writer"
Set-Location -Path $REPO_ROOT
& ${env:TEMP}\nuget.exe restore

## Build test library, which builds everything else transitively
& $MSBUILD vipr-t4templatewriter.sln /target:test\T4TemplateWriterTests:Rebuild

## Execute integration test
Set-Location "${REPO_ROOT}\test\T4TemplateWriterTests\bin\Debug"
& .\T4TemplateWriterTests.exe

Set-Location $REPO_ROOT