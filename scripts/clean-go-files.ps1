$directories = Get-ChildItem -Path $env:MainDirectory -Directory -Exclude @("core", "scripts", ".github" , "tests")
foreach ($directory in $directories) {
	Remove-Item -Path $directory.FullName -Recurse -Force -Verbose -Exclude *change_notification*, "change_type.go", "lifecycle_event_type.go", "resource_data.go", "resource_permission.go" , "resource_permission_collection_response.go"
}
Remove-Item -Path $env:MainDirectory -Filter "*.go" -Exclude "graph_request_adapter.go", "graph_service_client.go" -Verbose

Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green
