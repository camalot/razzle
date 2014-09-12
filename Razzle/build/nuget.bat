call "C:\Program Files (x86)\Microsoft Visual Studio 12.0\VC\vcvarsall.bat" x86

REM Builds the projects making sure they are up to date.

msbuild Build.msbuild /p:Project=Razzle /verbosity:Diagnostic

REM this will only build the package
msbuild NugetPublish.msbuild /p:PublishMode=NuGet /p:Project=Razzle /p:ProjectSharedName=Razzle /p:NuSpecFile=Razzle.nuspec  /p:LocalDeploy=E:\Development\deploy\nuget\ /verbosity:Diagnostic
pause