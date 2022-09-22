$directories = Get-ChildItem -Path $env:MainDirectory -Directory -Exclude @("core", "scripts", ".github")
foreach ($directory in $directories) {
	Remove-Item -Path $directory.FullName -Recurse -Force -Verbose
}
Get-ChildItem -Path $env:MainDirectory -Filter graph_base_service_client.go -Recurse | Remove-Item

Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green
