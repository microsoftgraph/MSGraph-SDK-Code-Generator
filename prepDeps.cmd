git clone https://github.com/microsoft/vipr %TEMP%\vipr
cmd /C %TEMP%\vipr\build.cmd
mkdir .\dependencies
copy /Y %TEMP%\vipr\src\Core\Vipr\bin\Debug\Vipr.exe .\dependencies\Vipr.exe
copy /Y %TEMP%\vipr\src\Core\Vipr\bin\Debug\Vipr.Core.dll .\dependencies\Vipr.Core.dll
copy /Y %TEMP%\vipr\src\Core\Vipr\bin\Debug\ODataReader.v4.dll .\dependencies\ODataReader.v4.dll
rmdir /S /Q %TEMP%\vipr