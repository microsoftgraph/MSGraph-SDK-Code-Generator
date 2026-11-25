# Copyright (c) Microsoft Corporation. All Rights Reserved. Licensed under the MIT License.
param([string]$FixtureRoot = [System.IO.Path]::GetTempPath())

$ErrorActionPreference = 'Stop'
$repoRoot = Split-Path (Split-Path $PSScriptRoot -Parent) -Parent
$generationScript = Join-Path $repoRoot 'scripts/generate-open-api.ps1'
$publicationScript = Join-Path $repoRoot 'scripts/git-push-cleanmetadata.ps1'
$testRoot = Join-Path $FixtureRoot "fairfax-pipeline-tests-$([guid]::NewGuid())"
$environmentNames = @('PublishChanges', 'PublishPath', 'PublicationIdentifier', 'CreateOpenAPIPR', 'BUILD_BUILDID', 'BUILD_REASON')
$originalEnvironment = @{}
foreach ($name in $environmentNames) {
    $originalEnvironment[$name] = [Environment]::GetEnvironmentVariable($name)
}

function Assert-Equal($actual, $expected, [string]$message) {
    if ($actual -ne $expected) {
        throw "$message`: expected '$expected', got '$actual'."
    }
}

function Invoke-TestGit {
    param([string[]]$Arguments)
    $output = & git @Arguments 2>&1
    if ($LASTEXITCODE -ne 0) {
        throw "Test git command failed: $($Arguments -join ' ')`n$output"
    }
    return $output
}

$hidiState = @{ Arguments = @(); ExitCode = 0 }
$function:hidi = {
    $hidiState.Arguments = @($args)
    $inputFile = $args[[Array]::IndexOf($args, '--csdl') + 1]
    $outputFile = $args[[Array]::IndexOf($args, '--output') + 1]
    if (!(Test-Path $inputFile)) {
        throw "Unexpected CSDL input: $inputFile"
    }
    Set-Content -LiteralPath $outputFile -Value "servers:`n  - url: http://localhost"
    $global:LASTEXITCODE = $hidiState.ExitCode
}.GetNewClosure()

New-Item -ItemType Directory -Path $testRoot | Out-Null
Push-Location $testRoot
try {
    foreach ($version in @('v1.0', 'beta')) {
        $metadataDirectory = "clean_$($version.Replace('.', ''))_metadata"
        New-Item -ItemType Directory -Path $metadataDirectory | Out-Null
        Set-Content (Join-Path $metadataDirectory "cleanMetadataWithDescriptionsAndAnnotations$version.xml") '<edmx />'
        & $generationScript -endpointVersion $version -settings 'default.json' -platformName default
        Assert-Equal (Get-Content "openapi/$version/default.yaml" -Raw).Trim() "servers:`n  - url: https://graph.microsoft.com/$version" 'Prod defaults changed'
        Assert-Equal $hidiState.Arguments[[Array]::IndexOf($hidiState.Arguments, '--metadata-version') + 1] $version 'Prod metadata version changed'
    }

    $fairfaxMetadata = Join-Path $testRoot 'Fairfax metadata'
    $fairfaxOutput = Join-Path $testRoot 'openapi/fairfax/v1.0'
    New-Item -ItemType Directory -Path $fairfaxMetadata | Out-Null
    Set-Content (Join-Path $fairfaxMetadata 'cleanMetadataWithDescriptionsAndAnnotationsv1.0.xml') '<edmx />'
    Set-Content (Join-Path $fairfaxMetadata 'cleanMetadataWithDescriptionsAndAnnotationsAndErrorsv1.0.xml') '<edmx />'
    $fairfaxArguments = @{
        endpointVersion = 'v1.0'
        metadataDirectory = $fairfaxMetadata
        outputDirectory = $fairfaxOutput
        serviceRoot = 'https://graph.microsoft.us/v1.0'
    }
    foreach ($platform in @('default', 'graphexplorer', 'openapi', 'powershell_v2')) {
        & $generationScript @fairfaxArguments -settings "$platform.json" -platformName $platform
        $expectedFile = 'cleanMetadataWithDescriptionsAndAnnotationsv1.0.xml'
        if ($platform -eq 'openapi') {
            $expectedFile = 'cleanMetadataWithDescriptionsAndAnnotationsAndErrorsv1.0.xml'
        }
        Assert-Equal $hidiState.Arguments[[Array]::IndexOf($hidiState.Arguments, '--csdl') + 1] (Join-Path $fairfaxMetadata $expectedFile) 'Incorrect Fairfax annotation variant'
        Assert-Equal $hidiState.Arguments[[Array]::IndexOf($hidiState.Arguments, '--metadata-version') + 1] 'v1.0' 'Cloud identity leaked into metadata version'
        Assert-Equal (Get-Content (Join-Path $fairfaxOutput "$platform.yaml") -Raw).Trim() "servers:`n  - url: https://graph.microsoft.us/v1.0" 'Incorrect Fairfax service URL'
    }

    $originalOpenApi = Get-Content (Join-Path $fairfaxOutput 'openapi.yaml') -Raw
    $hidiState.ExitCode = 1
    $failure = $null
    try {
        & $generationScript @fairfaxArguments -settings 'openapi.json' -platformName openapi
    }
    catch {
        $failure = $_
    }
    if (!$failure) {
        throw 'Hidi failure was reported as success.'
    }
    Assert-Equal (Get-Content (Join-Path $fairfaxOutput 'openapi.yaml') -Raw) $originalOpenApi 'Previous OpenAPI was not restored after failure'
    Assert-Equal (Test-Path (Join-Path $fairfaxOutput 'openapi.yaml.old')) $false 'Backup remained after restoration'

    $remote = Join-Path $testRoot 'remote.git'
    Invoke-TestGit -Arguments @('init', '--bare', '--initial-branch=master', $remote) | Out-Null
    Invoke-TestGit -Arguments @('clone', $remote, 'seed') | Out-Null
    Push-Location 'seed'
    try {
        Invoke-TestGit -Arguments @('config', 'user.name', 'Pipeline test') | Out-Null
        Invoke-TestGit -Arguments @('config', 'user.email', 'pipeline-test@example.invalid') | Out-Null
        Set-Content 'README.txt' 'Seed repository'
        Invoke-TestGit -Arguments @('add', 'README.txt') | Out-Null
        Invoke-TestGit -Arguments @('commit', '-m', "Seed fixture`n`nCo-authored-by: Copilot App <223556219+Copilot@users.noreply.github.com>") | Out-Null
        Invoke-TestGit -Arguments @('push', 'origin', 'master') | Out-Null
    }
    finally {
        Pop-Location
    }
    Invoke-TestGit -Arguments @('clone', $remote, 'publication') | Out-Null
    Push-Location 'publication'
    try {
        Invoke-TestGit -Arguments @('config', 'user.name', 'Pipeline test') | Out-Null
        Invoke-TestGit -Arguments @('config', 'user.email', 'pipeline-test@example.invalid') | Out-Null
        Invoke-TestGit -Arguments @('checkout', '--detach') | Out-Null
        $env:PublishChanges = 'True'
        $env:PublishPath = 'clean_v10_fairfax_metadata'
        $env:PublicationIdentifier = 'Fairfax/v1.0'
        $env:CreateOpenAPIPR = 'False'
        $env:BUILD_BUILDID = '123'
        $env:BUILD_REASON = 'Manual'
        New-Item -ItemType Directory -Path $env:PublishPath | Out-Null
        Set-Content "$env:PublishPath/cleanMetadataWithDescriptionsv1.0.xml" 'Fairfax CSDL'
        Set-Content 'unrelated.txt' 'Must not be published'
        & $publicationScript
        Assert-Equal ((Invoke-TestGit -Arguments @('--git-dir', $remote, 'show', 'master:clean_v10_fairfax_metadata/cleanMetadataWithDescriptionsv1.0.xml')) -join "`n") 'Fairfax CSDL' 'First-run cleaned CSDL was not pushed'
        Assert-Equal (Test-Path 'unrelated.txt') $true 'Unrelated file was lost'
        Assert-Equal ((Invoke-TestGit -Arguments @('--git-dir', $remote, 'ls-tree', '--name-only', 'master', 'unrelated.txt')) -join '') '' 'Unrelated file was committed'

        $env:PublishPath = 'openapi/fairfax/v1.0'
        $env:CreateOpenAPIPR = 'True'
        New-Item -ItemType Directory -Path $env:PublishPath -Force | Out-Null
        Set-Content "$env:PublishPath/default.yaml" 'Fairfax OpenAPI'
        $env:PublishChanges = 'False'
        $before = (Invoke-TestGit -Arguments @('rev-parse', 'HEAD')) -join ''
        & $publicationScript
        Assert-Equal ((Invoke-TestGit -Arguments @('rev-parse', 'HEAD')) -join '') $before 'Disabled publication created a commit'
        Assert-Equal (Get-Content "$env:PublishPath/default.yaml") 'Fairfax OpenAPI' 'Disabled publication lost generated files'

        $env:PublishChanges = 'True'
        & $publicationScript
        $branch = 'publish-open-api-files/123/Fairfax/v1.0'
        Assert-Equal ((Invoke-TestGit -Arguments @('--git-dir', $remote, 'show', "${branch}:openapi/fairfax/v1.0/default.yaml")) -join "`n") 'Fairfax OpenAPI' 'First-run OpenAPI was not pushed to its isolated branch'
        Assert-Equal ((Invoke-TestGit -Arguments @('--git-dir', $remote, 'ls-tree', '--name-only', 'master', 'openapi')) -join '') '' 'OpenAPI was pushed directly to master'
        $before = (Invoke-TestGit -Arguments @('rev-parse', 'HEAD')) -join ''
        & $publicationScript
        Assert-Equal ((Invoke-TestGit -Arguments @('rev-parse', 'HEAD')) -join '') $before 'No-change publication created a commit'

        Set-Content "$env:PublishPath/default.yaml" 'Updated Fairfax OpenAPI'
        Invoke-TestGit -Arguments @('remote', 'set-url', 'origin', (Join-Path $testRoot 'missing-remote.git')) | Out-Null
        $failure = $null
        try {
            & $publicationScript
        }
        catch {
            $failure = $_
        }
        if (!$failure) {
            throw 'Git publication failure was reported as success.'
        }
    }
    finally {
        Pop-Location
    }
    Write-Host 'Passed: Prod defaults, four Fairfax profiles, Hidi failure recovery, first-run CSDL/OpenAPI publication, publication controls, and Git failure propagation.'
}
finally {
    Pop-Location
    foreach ($name in $environmentNames) {
        [Environment]::SetEnvironmentVariable($name, $originalEnvironment[$name])
    }
    Remove-Item -LiteralPath $testRoot -Recurse -Force
}
