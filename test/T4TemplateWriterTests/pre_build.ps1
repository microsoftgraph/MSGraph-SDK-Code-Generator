$REPO_ROOT = Resolve-Path -Path '..\..'
$ASM_INFO_FILE = '{0}\submodules\vipr\src\Core\Vipr\Properties\AssemblyInfo.cs' -f $REPO_ROOT
$VIPR_CSPROJ = '{0}\submodules\vipr\src\Core\Vipr\Vipr.csproj' -f $REPO_ROOT
$ASM_TEST = 'T4TemplateWriterTests'
$INSTRUCTION_TO_ADD = '[assembly: InternalsVisibleTo("{0}")]' -f $ASM_TEST

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

## Restore nuget packages *within* submodule
## This is needed so relative paths in CSPROJ files work
Set-Location -Path "${REPO_ROOT}\submodules\vipr"
.\.nuget\nuget.exe restore

Set-Location -Path $REPO_ROOT
.\.nuget\nuget.exe restore