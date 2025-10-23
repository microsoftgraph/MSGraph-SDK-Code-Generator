#<##.Synopsis
#   Generate OpenAPI description from clean CSDL.
#   Note: this script assumes it's running at the root of the msgraph-metadata repo.
#
#.Description
#   This script is intended to be run from the root of the msgraph-metadata repo in an Azure Pipeline.
#
#.Parameter endpointVersion
#   Specifies the metadata endpoint to target. Expected values are "v1.0" and "beta"
#
#.Parameter settings
#   Specifies the configuration settings used to transform the csdl. Expected values are inside the configuration settings folder
#
#.Parameter platformName
#   Specifies the file name of the configuration settings used to transform the csdl. Will be used to separate the different output folders
#>

param(
    [parameter(Mandatory = $true)][String]$endpointVersion,
    [parameter(Mandatory = $true)][String]$settings,
    [parameter(Mandatory = $true)][String]$platformName
    )

Write-Host "Starting $endpointVersion OpenAPI generation for $platformName using generate-open-api.ps1"

$outputFile = Join-Path "./" "openapi" $endpointVersion "$platformName.yaml"
$oldOutputFile = "$outputFile.old"
$cleanVersion = $endpointVersion.Replace(".", "")

# The clean metadata file name is different for openapi and other versions
$baseFileName = "cleanMetadataWithDescriptionsAndAnnotations";
if($platformName -eq "openapi")
{
    $baseFileName += "AndErrors";
}
$fileName = "$baseFileName$endpointVersion.xml";

$inputFile = Join-Path "./" "clean_$($cleanVersion)_metadata" "$fileName"
Write-Host "`nSettings: $settings"
Write-Host "Generating OpenAPI description from $inputFile"
Write-Host "Output file: $outputFile"

if(Test-Path $outputFile)
{
    Write-Host "`nRemoving existing output file"
    if(Test-Path $oldOutputFile)
    {
        Write-Host "Removing existing old output file: $oldOutputFile"
        Remove-Item $oldOutputFile -Force
    }
    $oldFileName = Split-Path $outputFile -leaf
    $oldFileName += ".old"
    Rename-Item $outputFile $oldFileName
}

Write-Host "`nGenerating OpenAPI description using hidi..."
$command = "hidi transform --csdl ""$inputFile"" --output ""$outputFile"" --settings-path ""$settings"" --version ""3.0"" --metadata-version ""$endpointVersion"" --log-level Information --format yaml"
Write-Host $command

try {
    Invoke-Expression "$command"
    # temporary fix for the server url https://github.com/microsoftgraph/msgraph-metadata/issues/124
    $content = Get-Content $outputFile -Raw
    $updatedContent = $content -replace "http://localhost", "https://graph.microsoft.com/$endpointVersion"
    Set-Content $outputFile $updatedContent -NoNewline
    if(Test-Path $oldOutputFile)
    {
        Write-Host "`nRemoving existing old output file: $oldOutputFile"
        Remove-Item $oldOutputFile -Force
    }
    Write-Host "Completed generating OpenAPI description using hidi"
} catch {
    if(Test-Path $oldOutputFile)
    {
        Write-Host "`nRestoring old output file: $oldOutputFile"
        $originalFileName = Split-Path $outputFile -leaf
        Rename-Item $oldOutputFile $originalFileName
    }
    Write-Error "Error generating OpenAPI description: $_"
    throw $_
}

