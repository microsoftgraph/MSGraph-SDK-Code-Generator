$REPO_ROOT = '..\..'
$ASM_INFO_FILE = '.\submodules\vipr\src\Core\Vipr\Properties\AssemblyInfo.cs'
$ASM_TEST = 'T4TemplateWriterTests'
$INSTRUCTION_TO_ADD = '[assembly: InternalsVisibleTo("{0}")]' -f $ASM_TEST

$InvalidProjRef = @'
<ProjectReference Include="..\..\Writers\Vipr.Writer.CSharp\Vipr.Writer.CSharp.csproj">
      <Project>{ea55efcf-3127-4e85-9a37-a645c06ffcc1}</Project>
      <Name>Vipr.Writer.CSharp</Name>
</ProjectReference>
'@

$InstructionAdded = Select-String -Path "${REPO_ROOT}\${ASM_INFO_FILE}" -Pattern ('\[assembly: InternalsVisibleTo\("{0}"\)\]' -f $ASM_TEST) -Quiet
if (-not $InstructionAdded) {
    Add-Content -Path "${REPO_ROOT}\${ASM_INFO_FILE}" -Value $INSTRUCTION_TO_ADD
    Write-Host -Object "Instruction added to Core\Vipr\Properties\AssemblyInfo.cs."
} else {
    Write-Host -Object "Instruction already present in Core\Vipr\Properties\AssemblyInfo.cs."
}
