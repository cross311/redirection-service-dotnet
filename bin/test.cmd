
@if %_echo%!==! echo off

@rem ---------------------------------------------------------------------------------
@rem if user does not set MSBuildPath environment variable, we will try default
@rem ---------------------------------------------------------------------------------
if "%MSBuildPath%" == "" (
set MSBuildPath=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\
)

if not exist "%MSBuildPath%msbuild.exe" (
echo. msbuild.exe is not found on this machine. 
goto :eof
)

if exist "TestResults\results.trx" (
 DEL TestResults\results.trx
)


%MSBuildPath%msbuild.exe bin\runtests.xml /t:RunTests /p:Configuration=Release