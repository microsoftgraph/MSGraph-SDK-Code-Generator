#
# Creates the Chocolatey package version based on assembly version,
# and whether there is already a CI revision set.
#
param([String]$pat, [String]$targetPackageName = 'typewriter')

# Get the assembly version from the built library.
$pathToTypewriter = $env:Build_SourcesDirectory + '\src\Typewriter\bin\Release\Typewriter.exe'
$fileInfo = [Diagnostics.FileVersionInfo]::GetVersionInfo($pathToTypewriter)
$assemblySemver = $fileInfo.FileVersion.ToString()

Write-Host "Target package: $targetPackageName" -ForegroundColor Green

Write-Host "Assembly version: $assemblySemver" -ForegroundColor Green

$finalPackageVersion = ""
$ciToken = "-ci"

# Get information about the targetPackage from the Feed/Packages Azure DevOps REST API
# https://docs.microsoft.com/en-us/rest/api/vsts/packaging/packages/list?view=vsts-rest-5.0
# f887743a-88c6-4716-b4aa-50761b543a15 is the MicrosoftGraph package feed.
$feedQuery = 'https://feeds.dev.azure.com/o365exchange/_apis/packaging/Feeds/f887743a-88c6-4716-b4aa-50761b543a15/packages?api-version=5.0-preview.1'
$headerValue = ":" + $pat
$encodedPat = [Convert]::ToBase64String([Text.Encoding]::UTF8.GetBytes($headerValue))
$webclient = new-object System.Net.WebClient
$webclient.Headers.Add("Authorization", "Basic " + $encodedPat)

$jsonObject = $webclient.DownloadString($feedQuery) | ConvertFrom-Json

# Query the JSON object and get the package identifier for the target package name.
# $packageID = ($jsonObject.value | ? normalizedName -eq $targetPackageName).id

# Query the results of the Feed/Packages call for our target package.
$packageFeedNormalizedVersion = ($jsonObject.value | ? normalizedName -eq $targetPackageName).versions.normalizedVersion

Write-Host "Package feed version: $packageFeedNormalizedVersion" -ForegroundColor Green

# The string to split could be in the following two forms: "n.n.n" or "n.n.n-ci*"
$packageFeedNormalizedVersionParts = $packageFeedNormalizedVersion.Split('-')

# Check whether the assembly version and feed version match.
if ($packageFeedNormalizedVersionParts[0] -eq $assemblySemver) {
    # The assembly version and feed version match.
     
    # Check whether the latest package in the feed has been updated with a ci release, eg '-ci*'
    if ($packageFeedNormalizedVersionParts.length -eq 1) {
        # Append the starting ci revision.
        $finalPackageVersion = $assemblySemver + $ciToken + "0"
    } 
    else {
        # increment the ci revision, eg "0.1.2-ci3" become "0.1.2-ci4"
        $number = $packageFeedNormalizedVersionParts[1].Remove(0, 2) -as [int] # removing 'ci'    
        $finalPackageVersion = $assemblySemver + $ciToken + ($number += 1)
    }

}
# The don't match so just use the assembly version.
else {
    $finalPackageVersion = $assemblySemver
}

Write-Host "Final package version: $finalPackageVersion" -ForegroundColor Green

# Create Typewriter package
choco pack Typewriter.nuspec --version=$finalPackageVersion --out=$env:Build_ArtifactStagingDirectory