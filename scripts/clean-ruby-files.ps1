$directories = Get-ChildItem -Path $env:MainDirectory -Directory
foreach ($directory in $directories) {
	Remove-Item -Path $directory.FullName -Recurse -Force -Verbose
}
Get-ChildItem -Path $env:MainDirectory -Directory | ForEach-Object { Remove-Item -Path $_.FullName -Verbose -Force -Recurse}
Get-ChildItem -Path $env:MainDirectory -Exclude "graph_request_adapter.rb", "graph_service_client.rb", "version_information.rb" | Where-Object { !$_.PSIsContainer -and $_.Name -like "*.rb" } | ForEach-Object { Remove-Item -Path $_.FullName -Verbose -Force}

Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green
