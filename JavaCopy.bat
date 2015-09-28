LOCAL
SET SRCDIR=C:\Users\pnied\Documents\git\vipr-t4templatewriter\src\T4TemplateWriter\bin\debug\output\com\oneDrive
SET DESTDIR=C:\Users\pnied\Documents\git\onedrive-explorer-android\onedrivesdk\src\main\java\com\onedrive\sdk

ROBOCOPY %SRCDIR%\generated  %DESTDIR%\generated  /MIR
ROBOCOPY %SRCDIR%\extensions %DESTDIR%\extensions /MIR

ENDLOCAL
