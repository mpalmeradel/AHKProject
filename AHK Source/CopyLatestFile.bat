@echo off
REM set source="C:\Users\mpalm\Videos\Lync Recordings"
REM set target="C:\Temp"

set /p source=
set /p target=

REM FOR /F "delims=" %%I IN ('DIR %source%\*.* /A:-D /O:-D /B') DO COPY %source%\"%%I" %target% & echo %%I & GOTO :END
FOR /F "delims=" %%I IN ('DIR "%source%"\*.* /A:-D /O:-D /B') DO COPY "%source%"\"%%I" "%target%" & echo %%I & GOTO :END
:END

REM TIMEOUT 4