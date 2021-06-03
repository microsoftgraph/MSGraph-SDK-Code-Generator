# Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

# This script runs typewriter which results in: 1) metadata processed through an xslt and 2) documentation added as annotations in the metadata

Push-Location -Path $env:TypewriterDirectory

& chmod +x $env:TypewriterExecutable
& $env:TypewriterExecutable -v Info -m $env:InputMetadataFile -o $env:OutputPath -g $env:GenerationMode -t $env:Transform -d $env:DocsDirectory -e $env:endpointVersion -r $env:RemoveAnnotations -f $env:OutputMetadataFileName

Pop-Location