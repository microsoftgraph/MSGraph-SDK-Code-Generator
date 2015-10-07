LOCAL
SET SRCDIR="%userprofile%\Documents\git\vipr-t4templatewriter\src\T4TemplateWriter\bin\debug\output\com\oneDrive"
SET DESTDIR="%userprofile%\Documents\git\onedrive-explorer-android\onedrivesdk\src\main\java\com\onedrive\sdk"

ROBOCOPY %SRCDIR%\generated  %DESTDIR%\generated  /MIR
ROBOCOPY %SRCDIR%\extensions %DESTDIR%\extensions /MIR

ENDLOCAL
