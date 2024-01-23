<#
.Synopsis
    Script meant to help create a new root segment package in the repo
.Description
    This scritp is not meant to be used by CI, but only for manual additions of new root segments.
#>
param (
    [Parameter(Mandatory = $true)]
    [string]
    $rootPathSegment,
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [Parameter(Mandatory = $true)]
    [string]
    $sourcePathSegment = "appCatalogs",
    [string]
    $packageName = "@microsoft/msgraph-sdk-javascript"
)
Push-Location $targetDirectory
$finalPackageName = "$packageName-$rootPathSegment"
$sourceLocation = "packages/$($packageName.Split('/')[1])-$sourcePathSegment/"
$targetLocation = "packages/$($finalPackageName.Split('/')[1])/"
npx lerna create $finalPackageName --yes
$filesToCopy = @(
    "tsconfig.json",
    ".eslintignore",
    ".eslintrc.json",
    ".gitignore",
    ".npmignore",
    "index.ts"
)
foreach($file in $filesToCopy) {
    Copy-Item "$sourceLocation/$file" $targetLocation
}

$indexTsContent = Get-Content -Path "$sourceLocation/index.ts" -Raw
$indexTsContent = $indexTsContent.Replace($sourcePathSegment.Substring(0,1).ToUpper() + $sourcePathSegment.Substring(1), $rootPathSegment.Substring(0,1).ToUpper() + $rootPathSegment.Substring(1))
$indexTsContent = $indexTsContent.Replace($sourcePathSegment, $rootPathSegment)
Set-Content -Path "$($packagesDirectory.FullName)\index.ts" -Value $indexTsContent -NoNewline

$directoriesToRemove = @(
    "lib",
    "__tests__"
)
foreach($directory in $directoriesToRemove) {
    Remove-Item -r "$targetLocation/$directory"
}

$sourcePackageJson = Get-Content -Raw "$sourceLocation/package.json" | ConvertFrom-Json
$targetPackageJson = Get-Content -Raw "$targetLocation/package.json" | ConvertFrom-Json
$targetPackageJson = $targetPackageJson | Select-Object -ExcludeProperty "directories", "files"
$targetPackageJson.description = $sourcePackageJson.description
$targetPackageJson.keywords = $sourcePackageJson.keywords
$targetPackageJson.license = $sourcePackageJson.license
$targetPackageJson.main = $sourcePackageJson.main
$targetPackageJson.name = $finalPackageName.ToLower() #doing this here to the directory name follows the original name casing
$targetPackageJson.scripts = $sourcePackageJson.scripts
$targetPackageJson.version = $sourcePackageJson.version
$targetPackageJson | add-member -Name "types" -value $sourcePackageJson.types -MemberType NoteProperty

Set-Content -Path "$targetLocation/package.json" -Value ($targetPackageJson | ConvertTo-Json -Depth 100) -NoNewline

$dependencies = @(
    $packageName.ToLower(),
    "tslib",
    "guid-typescript",
    "@microsoft/kiota-abstractions",
    "@microsoft/kiota-serialization-form",
    "@microsoft/kiota-serialization-json",
    "@microsoft/kiota-serialization-multipart",
    "@microsoft/kiota-serialization-text"
)
foreach($dependency in $dependencies) {
    npm i $dependency -w $finalPackageName -S
    npm i $dependency -w $finalPackageName -S
}

$devDependencies = @("typescript")

foreach($dependency in $devDependencies) {
    npm i $dependency -w $finalPackageName -D
    npm i $dependency -w $finalPackageName -D
}
#doing it twice otherwise the dependency is not saved for som reason

Pop-Location