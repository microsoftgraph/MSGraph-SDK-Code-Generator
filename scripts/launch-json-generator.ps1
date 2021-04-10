$obj = @{}
$obj.version = "0.2.0"
$obj.configurations = @()

$languages = "CSharp","PHP","TypeScript","Java"
$endpoints = "beta","v1.0"
$metadata = @{
    "beta" = "`${workspaceFolder}/../msgraph-metadata/clean_beta_metadata/cleanMetadataWithDescriptionsbeta.xml";
    "v1.0" = "`${workspaceFolder}/../msgraph-metadata/clean_v10_metadata/cleanMetadataWithDescriptionsv1.0.xml"
}

foreach($language in $languages)
{
    foreach($endpoint in $endpoints)
    {
        $args = @(
            "-v",
            "Info",
            "-m",
            $metadata[$endpoint],
            "-g",
            "Files",
            "-l",
            $language,
            "-o",
            "`${workspaceFolder}/../Output$language$endpoint",
            "-e",
            $endpoint
        )

        if ($endpoint -eq "beta")
        {
            if ($language -eq "PHP")
            {
                $args += @(
                    "-p",
                    "php.namespacePrefix:Beta"
                )
            }
            elseif ($language -eq "TypeScript")
            {
                $args += @(
                    "-p",
                    "typescript.namespacePostfix:beta"
                )
            }
        }

        $configuration = @{
            name = "Generate $language $endpoint"
            type = "coreclr"
            request = "launch"
            preLaunchTask = "build"
            program = "`${workspaceFolder}/src/Typewriter/bin/Debug/net5.0/Typewriter.dll"
            args = $args
            cwd = "`${workspaceFolder}/src/Typewriter/bin/Debug/net5.0"
			console = "internalConsole"
			stopAtEntry = $false
        }

        $obj.configurations += $configuration
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
