# Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

Write-Host "Running Typewriter to clean the metadata..."
Push-Location -Path $env:TypewriterDirectory

Write-Host "Typewriter.exe: $env:TypewriterExecutable `nInput metadata: $env:InputMetadataFile `n output path: $env:OutputPath"

# Note: We were only inserting the v1.0 docs. This change will make us insert beta docs into beta code files.
& $env:TypewriterExecutable -v Info -m $env:InputMetadataFile -o $env:OutputPath -g $env:GenerationMode -t $env:Transform -d $env:DocsDirectory

Pop-Location