
@if %_echo%!==! echo off

em ---------------------------------------------------------------------------------
@rem if user does not set MSBuildPath environment variable, we will try default
@rem ---------------------------------------------------------------------------------
if "%MSBuildPath%" == "" (
set MSBuildPath=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\
)

if not exist "%MSBuildPath%msbuild.exe" (
echo. msbuild.exe is not found on this machine. 
goto :eof
)


%MSBuildPath%msbuild.exe src\RedirectionService.WebApi\RedirectionService.WebApi.csproj /t:Package /p:Configuration=Release;DesktopBuildPackageLocation=..\..\package\redirectionservice.zip;DeployIisAppPath="Default Web Site/Api"