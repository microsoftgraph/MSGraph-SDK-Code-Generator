$content = Get-Content -Path $env:BarrelFilePath
$lines = @(
    "require_relative 'microsoft_graph_service_client'",
    "require_relative 'microsoft_graph_request_adapter'",
    "require_relative 'models/models'"
)
if ($content[0] -ne $lines[0]) {
    Write-Host "inserting lines into $env:BarrelFilePath"
    $updatedContent = $lines + $content
    $updatedContent | Set-Content -Path $env:BarrelFilePath -Verbose
} else {
    Write-Host "barrel file is up to date"
}