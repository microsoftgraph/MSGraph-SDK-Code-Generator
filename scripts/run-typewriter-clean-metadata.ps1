# Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

# This script runs typewriter which results in: 1) metadata processed through an xslt and 2) documentation added as annotations in the metadata. It 
# optionally can remove annotations.

Push-Location -Path $env:TypewriterDirectory

& chmod +x $env:TypewriterExecutable

if (Test-Path env:RemoveAnnotations) # Use the XSLT with a flag to remove capability annotations, and add documentation annotations to the metadata.
{
    & $env:TypewriterExecutable -v Info -m $env:InputMetadataFile -o $env:OutputPath -g $env:GenerationMode -t $env:Transform -d $env:DocsDirectory -e $env:endpointVersion -r $env:RemoveAnnotations -f $env:OutputMetadataFileName
}
else # Use the XSLT with default transform values and add documentation annotations to the metadata.
{
    & $env:TypewriterExecutable -v Info -m $env:InputMetadataFile -o $env:OutputPath -g $env:GenerationMode -t $env:Transform -d $env:DocsDirectory -e $env:endpointVersion
}

Pop-Location