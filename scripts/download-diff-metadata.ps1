# Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

Push-Location $env:BUILD_SOURCESDIRECTORY/msgraph-metadata

function Format-Xml {
    <#
    .SYNOPSIS
    Format the incoming object as the text of an XML document.
    #>
    param(
        ## Text of an XML document.
        [Parameter(ValueFromPipeline = $true)]
        [string[]]$Text
    )
    
    begin {
        $data = New-Object System.Collections.ArrayList
    }
    process {
        [void] $data.Add($Text -join "`n")
    }
    end {

        $doc = New-Object System.Xml.XmlDocument
        $doc.LoadXml($data -join "`n")
        
        $memStream = New-Object System.IO.MemoryStream
        $writer = New-Object System.Xml.XmlTextWriter($memStream, [System.Text.Encoding]::UTF8)
        $writer.Formatting = [System.Xml.Formatting]::Indented
        $doc.WriteContentTo($writer)
        $writer.Flush()
        $memStream.Flush()

        $memStream.Position = 0
        $sReader = New-Object System.IO.StreamReader($memStream)

        return $sReader.ReadToEnd()
    }
}

# Are we on master? If not, we will want our changes committed on master.
$branch = &git rev-parse --abbrev-ref HEAD
Write-Host "Current branch: $branch"
if ($branch -ne $env:targetBranch) {
    git fetch origin $env:targetBranch | Write-Host
    git checkout $env:targetBranch | Write-Host
    $branch = &git rev-parse --abbrev-ref HEAD
    Write-Host "Current branch: $branch"
    git pull origin $env:targetBranch --ff-only | Write-Host
}

if ($env:inputMetadataFile.StartsWith("http", [StringComparison]::OrdinalIgnoreCase)){    
    # Download metadata from livesite.
    $url = "https://graph.microsoft.com/{0}/`$metadata" -f $env:endpointVersion
    $metadataFileName = "{0}_metadata.xml" -f $env:endpointVersion
    $pathToLiveMetadata = Join-Path -Path ($pwd).path -ChildPath $metadataFileName
    $client = new-object System.Net.WebClient
    $client.Encoding = [System.Text.Encoding]::UTF8
    Write-Host "Attempting to download metadata from $url to $pathToLiveMetadata" -ForegroundColor DarkGreen
    $client.DownloadFile($url, $pathToLiveMetadata)
    Write-Host "Downloaded metadata from $url to $pathToLiveMetadata" -ForegroundColor DarkGreen

    # Format the metadata to make it easy for us hoomans to read and perform non-markup line based diffs.
    $content = Format-Xml (Get-Content $pathToLiveMetadata)
    [IO.File]::WriteAllLines($pathToLiveMetadata, $content)
    Write-Host "Wrote $metadataFileName to disk. Now git will tell us whether there are changes." -ForegroundColor DarkGreen
}
else{
    # Retrieve the metadata from ./schemas folder in the metadata repo.
    Write-Host "Retrieving metadata from $env:inputMetadataFile" -ForegroundColor DarkGreen
    $content = Format-Xml (Get-Content $env:inputMetadataFile)
    Write-Host "Retrieved metadata from $env:inputMetadataFile" -ForegroundColor DarkGreen    
}

# Discover if there are changes between the downloaded file and what is in git.
[array]$result = git status --porcelain

# Check for expected and unexpected changes.
if ($result |Where {$_ -match '^\?\?'}) {
    Write-Error "Unexpected untracked file[s] exists. We shouldn't be adding new files via this script. Only modifying existing files."
}
elseif ($result |Where {$_ -notmatch '^\?\?'}) {
    Write-Host "Uncommitted changes are present." -ForegroundColor Yellow

    $hasUpdatedMetadata = $false
    Foreach ($r in $result) {

        if ($r.Contains($metadataFileName)) {
            $hasUpdatedMetadata = $true
            Break
        }
    }

    if (!$hasUpdatedMetadata) {
        Write-Error "Exit build. Uncommitted changes are present that do not match the expected file name."
        Exit
    }
}
else {
    # tree is clean
    # make sure that pipelines have failOnStderr:true and errorActionPreference:stop set.
    if ($env:errorOnNoChange -eq $true) {
        Write-Error "No changes reported. Cancel pipeline."
    } 
    Exit
}

git pull origin $env:targetBranch --recurse-submodules # sync changes in case someone else pushed to the same branch.
git add $metadataFileName | Write-Host
git commit -m "Updated $metadataFileName" | Write-Host
git push origin $env:targetBranch | Write-Host

Pop-Location