$content = Get-Content -Path $env:BarrelFilePath
$lines = @(
    "require_relative 'graph_service_client'",
    "require_relative 'graph_request_adapter'",
    "require_relative 'models/models'"
)
if ($content.Length -eq 0) {
    Write-Host "inserting lines into $env:BarrelFilePath"
    $lines | Set-Content -Path $env:BarrelFilePath -Verbose
} elseif ($content[0] -ne $lines[0]) {
    Write-Host "inserting lines into $env:BarrelFilePath"
    $updatedContent = $lines + $content
    $updatedContent | Set-Content -Path $env:BarrelFilePath -Verbose
} else {
    Write-Host "barrel file is up to date"
}