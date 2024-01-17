param (
    [Parameter(Mandatory = $true)]
    [string]
    $rootPathSegment,
    [Parameter(Mandatory = $true)]
    [string]
    $targetDirectory,
    [string]
    $packageName = "@microsoft/msgraph-sdk-javascript"
)
Push-Location $targetDirectory
$finalPackageName = "$packageName-$rootPathSegment"
$sourceLocation = "packages/$($packageName.Split('/')[1])-appCatalogs/"
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
$directoriesToRemove = @(
    "lib",
    "__tests__"
)
foreach($directory in $directoriesToRemove) {
    Remove-Item -r "$targetLocation/$directory"
}
# TODO update references in index.ts

$sourcePackageJson = Get-Content -Raw "$sourceLocation/package.json" | ConvertFrom-Json
$targetPackageJson = Get-Content -Raw "$targetLocation/package.json" | ConvertFrom-Json
$targetPackageJson = $targetPackageJson | Select-Object -ExcludeProperty "directories", "files"
$targetPackageJson.description = $sourcePackageJson.description
$targetPackageJson.keywords = $sourcePackageJson.keywords
$targetPackageJson.license = $sourcePackageJson.license
$targetPackageJson.main = $sourcePackageJson.main
$targetPackageJson.scripts = $sourcePackageJson.scripts
$targetPackageJson.version = $sourcePackageJson.version
$targetPackageJson | add-member -Name "types" -value $sourcePackageJson.types -MemberType NoteProperty

Set-Content -Path "$targetLocation/package.json" -Value ($targetPackageJson | ConvertTo-Json -Depth 100)

$dependencies = @(
    $packageName.ToLower(),
    "tslib",
    "guid-typescript",
    "D:\github\kiota-typescript\packages\abstractions",
    "D:\github\kiota-typescript\packages\http\fetch",
    "D:\github\kiota-typescript\packages\serialization\form",
    "D:\github\kiota-typescript\packages\serialization\json",
    "D:\github\kiota-typescript\packages\serialization\multipart",
    "D:\github\kiota-typescript\packages\serialization\text"
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