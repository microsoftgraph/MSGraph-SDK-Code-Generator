$CWD = Get-Location

function Invoke-NpmPack(){
    npm run clean
    npm install
    npm run build
    npm pack
}

Write-Host "Packaging typescript kiota-abstractions" -ForegroundColor Green

# Build kiota code
## kiota abstractions
$ABSTRACTIONS_PATH = "$CWD/kiota/abstractions/typescript/"
Set-Location $ABSTRACTIONS_PATH

Invoke-NpmPack

$ABSTRACTIONS_PACKAGE = (Get-ChildItem $ABSTRACTIONS_PATH -Recurse -Include microsoft-kiota-abstractions-*.tgz).Name

Write-Host "Packaging typescript kiota-fetch" -ForegroundColor Green

## kiota fetch
$FETCH_PATH = "$CWD/kiota/http/typescript/fetch/"
Set-Location $FETCH_PATH

### kiota fetch pre-requisites
Copy-Item $ABSTRACTIONS_PATH/$ABSTRACTIONS_PACKAGE $FETCH_PATH
npm install ./$ABSTRACTIONS_PACKAGE
### build fetch library
Invoke-NpmPack


Write-Host "Packaging typescript kiota-serialization" -ForegroundColor Green

## kiota serialization
$SERIALIZATION_PATH = "$CWD/kiota/serialization/typescript/json/"
Set-Location $SERIALIZATION_PATH
### kiota serialization pre-requisites
Copy-Item $ABSTRACTIONS_PATH/$ABSTRACTIONS_PACKAGE $SERIALIZATION_PATH
npm install ./$ABSTRACTIONS_PACKAGE
### build serialization library
Invoke-NpmPack


Write-Host "Copy typescript dependecies to sdk folder" -ForegroundColor Green

## copy all the kiota packages to the main typescript directory
$TYPESCRIPT_MAIN_DIR = "$CWD/msgraph-sdk-typescript/main/"
If(!(test-path $TYPESCRIPT_MAIN_DIR))
{
    New-Item -ItemType Directory -Force -Path $TYPESCRIPT_MAIN_DIR
} else {
    Remove-Item -LiteralPath $TYPESCRIPT_MAIN_DIR -Force -Recurse
    New-Item -ItemType Directory -Force -Path $TYPESCRIPT_MAIN_DIR
}

Copy-Item $ABSTRACTIONS_PATH/microsoft-kiota-abstractions-*.tgz $TYPESCRIPT_MAIN_DIR
Copy-Item $FETCH_PATH/microsoft-kiota-http-fetchlibrary-*.tgz $TYPESCRIPT_MAIN_DIR
Copy-Item $SERIALIZATION_PATH/microsoft-kiota-serialization-json-*.tgz $TYPESCRIPT_MAIN_DIR

Set-Location $CWD
