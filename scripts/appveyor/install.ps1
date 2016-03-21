Write-Host -Object ("Chocolatey version: {0}" -f $(choco -v))
choco install visualstudio2013-modelingsdk -y
