function Delete-Folder-Recursive {
    param (
        $FolderName
    )

    $directories = Get-ChildItem -Path $FolderName -Directory
    foreach ($directory in $directories) {
    	Delete-Folder-Recursive $directory.FullName
        Remove-Item -Path $directory.FullName -Recurse -Force -Verbose
    }

    Write-Host "Deleting folder: $FolderName"
}

Delete-Folder-Recursive $env:MainDirectory

$files = Get-ChildItem -Path $env:MainDirectory -File  -Filter "*.ts"
foreach ($file in $files) {
  Remove-Item -Path $file.FullName -Force
}


Write-Host "Removed the existing generated files in the repo's main directory: $env:MainDirectory" -ForegroundColor Green
