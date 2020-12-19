# Copyright (c) Microsoft Corporation.  All Rights Reserved.  Licensed under the MIT License.  See License in the project root for license information.

Write-Host "Running Typewriter..."
Push-Location -Path $env:TypewriterDirectory

& chmod +x $env:TypewriterExecutable

if ($env:Endpoint -eq "beta")
{
  if ($env:Language -eq "PHP")
  {
    & $env:TypewriterExecutable -v Info -m $env:CleanMetadataFile -o $env:OutputPath -g Files -l $env:Language -e beta -p php.namespacePrefix:Beta
  }
  elseif ($env:Language -eq "TypeScript")
  {
    & $env:TypewriterExecutable -v Info -m $env:CleanMetadataFile -o $env:OutputPath -g Files -l $env:Language -e beta -p typescript.namespacePostfix:beta
  }
  else
  {
    & $env:TypewriterExecutable -v Info -m $env:CleanMetadataFile -o $env:OutputPath -g Files -l $env:Language -e beta
  }
}
else
{
  & $env:TypewriterExecutable -v Info -m $env:CleanMetadataFile -o $env:OutputPath -g Files -l $env:Language
}

Pop-Location
