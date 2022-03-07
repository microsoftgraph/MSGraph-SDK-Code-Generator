# Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

# This script runs typewriter which results in: 1) metadata processed through an xslt and 2) documentation added as annotations in the metadata. It 
# optionally can remove annotations.

Push-Location -Path $env:TypewriterDirectory

& chmod +x $env:TypewriterExecutable

$additionalFlags = ""
if (Test-Path env:RemoveAnnotations) # Use the XSLT with a flag to remove capability annotations, and add documentation annotations to the metadata.
{
    $additionalFlags += " -r $env:RemoveAnnotations"
}
if(Test-Path env:AddInnerErrorDescription)
{
    $additionalFlags += " -a $env:AddInnerErrorDescription"
}

if($additionalFlags -ne "")
{
    $additionalFlags += " -f $env:OutputMetadataFileName"
}

$metadataFile = $env:InputMetadataFile -replace "\$", "```$" # To avoid considering $metadata as a variable in invoke expression

Invoke-Expression "$env:TypewriterExecutable -v Info -m $metadataFile -o $env:OutputPath -g $env:GenerationMode -t $env:Transform -d $env:DocsDirectory -e $env:endpointVersion$additionalFlags"

Pop-Location