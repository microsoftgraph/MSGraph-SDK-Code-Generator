$directories = Get-ChildItem -Path $env:MainDirectory -Directory
foreach ($directory in $directories) {
	Remove-Item -Path $directory.FullName -Recurse -Force -Verbose
}

$files = Get-ChildItem -Path $env:MainDirectory -File  -Filter "*.py"
foreach ($file in $files) {
  Remove-Item -Path $file.FullName -Force
}


Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green