function createCommandLineArgs
{
    param (
        [string]$metadata,
        [string]$endpoint,
        [string]$language,
        [string]$outputFolder
    )

    $commandLineArgs = @(
        "-v",
        "Info",
        "-m",
        $metadata,
        "-g",
        "Files",
        "-l",
        $language,
        "-o",
        $outputFolder,
        "-e",
        $endpoint
    )

    if ($endpoint -eq "beta")
    {
        if ($language -eq "PHP")
        {
            $commandLineArgs += @(
                "-p",
                "php.namespacePrefix:Beta"
            )
        }
        elseif ($language -eq "TypeScript")
        {
            $commandLineArgs += @(
                "-p",
                "typescript.namespacePostfix:beta"
            )
        }
    }

    return $commandLineArgs
}

function createConfiguration
{
    param (
        [string]$name,
        [string[]]$commandLineArgs
    )

    [ordered]@{
        name = $name
        type = "coreclr"
        request = "launch"
        preLaunchTask = "build"
        program = "`${workspaceFolder}/src/Typewriter/bin/Debug/net5.0/Typewriter.dll"
        args = $commandLineArgs
        cwd = "`${workspaceFolder}/src/Typewriter/bin/Debug/net5.0"
        console = "internalConsole"
        stopAtEntry = $false
    }
}

$obj = [ordered]@{
    version = "0.2.0"
    configurations = @()
}

$languages = "CSharp","PHP","TypeScript","Java","ObjC"
$endpoints = "beta","v1.0"
$prodMetadata = @{
    "beta" = "`${workspaceFolder}/../msgraph-metadata/clean_beta_metadata/cleanMetadataWithDescriptionsbeta.xml";
    "v1.0" = "`${workspaceFolder}/../msgraph-metadata/clean_v10_metadata/cleanMetadataWithDescriptionsv1.0.xml"
}

$metadataMultipleNamespacesFile = "`${workspaceFolder}/test/Typewriter.Test/Metadata/MetadataMultipleNamespaces.xml"
$metadataWithSubNamespacesFile = "`${workspaceFolder}/test/Typewriter.Test/Metadata/MetadataWithSubNamespaces.xml"

$testMetadata = @{
    CSharp = $metadataMultipleNamespacesFile
    Java = $metadataMultipleNamespacesFile
    TypeScript = $metadataWithSubNamespacesFile
    PHP = $metadataWithSubNamespacesFile
    ObjC = $metadataWithSubNamespacesFile
}

$testData = @{
    "beta" = "Java","PHP","TypeScript"
    "v1.0" = "CSharp","Java","ObjC","PHP","TypeScript"
}

foreach($language in $languages)
{
    foreach($endpoint in $endpoints)
    {
        $commandLineArgs = createCommandLineArgs $prodMetadata[$endpoint] $endpoint $language "`${workspaceFolder}/../generator-output-$language-$endpoint"
        $obj.configurations += createConfiguration "Generate $language $endpoint" $commandLineArgs

        if ($testData[$endpoint] -contains $language)
        {
            $postFix = ""
            if ($endpoint -eq "beta")
            {
                $postFix = "Beta"
            }

            $testDataCommandLineArgs = createCommandLineArgs $testMetadata[$language] $endpoint $language "`${workspaceFolder}/test/Typewriter.Test/TestData$language$postFix"
            $obj.configurations += createConfiguration "Update Test Data $language $endpoint" $testDataCommandLineArgs
        }
    }
}

$obj.configurations += @{
    name = ".NET Core Attach";
    type = "coreclr";
    request = "attach";
    processId = "`${command:pickProcess}"
}

$launchJsonPath = (Resolve-Path "$PSScriptRoot/../.vscode/launch.json").Path
$obj | ConvertTo-Json -Depth 5 > $launchJsonPath