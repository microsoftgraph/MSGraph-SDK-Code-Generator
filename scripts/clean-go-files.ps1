# Only generated model/request-builder directories should be removed before regeneration.
# Exclude hand-maintained folders and ALL dot-directories (".*" matches .github, .config,
# .azure-pipelines, .vscode, .devcontainer, etc.) so repo tooling, CI and security-baseline
# config are not deleted by generation runs.
$directories = Get-ChildItem -Path $env:MainDirectory -Directory -Exclude @("core", "scripts", "tests", ".*")
foreach ($directory in $directories) {
	Remove-Item -Path $directory.FullName -Recurse -Force -Verbose -Exclude *change_notification*, "change_type.go", "lifecycle_event_type.go", "resource_data.go", "resource_permission.go" , "resource_permission_collection_response.go"
}
Remove-Item -Path $env:MainDirectory -Filter "*.go" -Exclude "graph_request_adapter.go", "graph_service_client.go" -Verbose

Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green
