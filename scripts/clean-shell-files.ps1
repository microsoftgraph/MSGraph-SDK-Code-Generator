$directories = Get-ChildItem -Path $env:MainDirectory -Directory -Exclude @("core", "scripts", ".github", ".git")
foreach ($directory in $directories) {
	Remove-Item -Path $directory.FullName -Recurse -Force -Verbose
}
Remove-Item -Path $env:MainDirectory -Filter "*.cs" -Exclude "Program.cs" -Verbose

Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green
