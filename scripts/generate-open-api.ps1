#<#
#.Synopsis
#   Generate OpenAPI description from clean CSDL.
#   Note: this script assumes it's running at the root of the msgraph-metadata repo.
#
#.Description
#   This script is intended to be run from the root of the msgraph-metadata repo in an Azure Pipeline.
#
#.Parameter endpointVersion
#   Specifies the metadata endpoint to target. Expected values are "v1.0" and "beta"
#>

param([parameter(Mandatory = $true)][String]$endpointVersion)

$outputFile = Join-Path "./" "openapi" $endpointVersion "openapi.yaml"
$oldOutputFile = "$outputFile.old"
$cleanVersion = $endpointVersion.Replace(".", "")
$inputFile = Join-Path "./" "clean_$($cleanVersion)_metadata" "cleanMetadataWithDescriptionsAndAnnotationsAndErrors$endpointVersion.xml"

Write-Verbose "Generating OpenAPI description from $inputFile"
Write-Verbose "Output file: $outputFile"

if(Test-Path $outputFile)
{
    Write-Verbose "Removing existing output file"
    if(Test-Path $oldOutputFile)
    {
        Write-Verbose "Removing existing old output file"
        Remove-Item $oldOutputFile -Force
    }
    $oldFileName = Split-Path $outputFile -leaf
    $oldFileName += ".old"
    Rename-Item $outputFile $oldFileName
}

try {
    Invoke-Expression "hidi transform --csdl ""$inputFile"" --output ""$outputFile"" --version OpenApi3_0 --loglevel Information --format yaml"
    # temporary fix for the server url https://github.com/microsoftgraph/msgraph-metadata/issues/124
    $content = get-content $outputFile
    $updatedContent = $content -replace "http://localhost", "https://graph.microsoft.com/$endpointVersion"
    Set-Content $outputFile $updatedContent
    if(Test-Path $oldOutputFile)
    {
        Write-Verbose "Removing existing old output file"
        Remove-Item $oldOutputFile -Force
    }
} catch {
    if(Test-Path $oldOutputFile)
    {
        Write-Warning "Restoring old output file"
        $originalFileName = Split-Path $outputFile -leaf
        Rename-Item $oldOutputFile $originalFileName
    }
    Write-Error "Error generating OpenAPI description: $_"
    throw $_
}
