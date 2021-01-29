$extensionsAndGeneratedDirectories = Get-ChildItem $env:MainDirectory -Include extensions,generated -Recurse -Directory

# this list should be updated if a new hand-crafted extension is added to one of the extensions/ directories
$filesThatShouldNotBeDeleted = "UploadSession.java","DateOnly.java","TimeOfDay.java","Multipart.java","ChunkedUploadRequest.java","ChunkedUploadResult.java","CustomRequestBuilder.java"
foreach ($directory in $extensionsAndGeneratedDirectories)
{
    Remove-Item $directory.FullName -Recurse -Force -Exclude $filesThatShouldNotBeDeleted
}
Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green