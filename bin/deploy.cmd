@if %_echo%!==! echo off

SETLOCAL ENABLEEXTENSIONS

if "%PackageLocation%" == "" (

  set PackageLocation=package

)

set _DeploySetParametersFile=%PackageLocation%\redirectionservice.SetParameters.xml

set DeployParams=

if "%DeployComputerName%" == "" (

echo.DeployComputerName is not set in the environment.  Deploying to localhost.

) else (

set DeployParams=%DeployParams% /M:%DeployComputerName%

)

if "%DeployUserName%" == "" (

echo.DeployUserName is not set in the environment. Using logged in user.

) else (

if "%DeployPassword%" == "" (

  echo.DeployPassword is not set in the environment. Please specify the user's password on the remote computer to deploy with.

  goto :eof

)

set DeployParams=%DeployParams% /U:%DeployUserName% /P:%DeployPassword%

)

CALL powershell .\bin\SetParameterFromEnv.ps1 -inputFile "%_DeploySetParametersFile%" -outputFile "%_DeploySetParametersFile%"

echo.-----------------------------------------------------------------
echo.Going to deploy using web deploy. Command:
echo."%PackageLocation%\webdeploy.cmd" /Y %DeployParams%
echo.-----------------------------------------------------------------

"%PackageLocation%\webdeploy.cmd" /Y %DeployParams%

echo.Deploy exited with error level: %errorlevel%
if %errorlevel% neq 0 exit /b %errorlevel%