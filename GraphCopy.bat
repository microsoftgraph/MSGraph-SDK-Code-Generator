LOCAL
SET SRCDIR=%userprofile%\Documents\git\vipr-t4templatewriter\src\T4TemplateWriter\bin\debug\output\com\Microsoft\Graph
SET DESTDIR=%userprofile%\Documents\git\onedrive-explorer-android\onedrivesdk\src\main\java\com\microsoft\graph\sdk

ROBOCOPY "%SRCDIR%\generated"  "%DESTDIR%\generated"
ROBOCOPY "%SRCDIR%\extensions" "%DESTDIR%\extensions"

ENDLOCAL
