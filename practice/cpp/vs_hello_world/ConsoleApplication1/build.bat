@echo off
pushd "C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\VC\Auxiliary\Build\"
call vcvarsall.bat x64     
popd
msbuild %*

REM set compilerflags=/Od /Zi /EHsc
REM set linkerflags=/OUT:hello.exe
REM cl.exe %compilerflags% %* ConsoleApplication1\ConsoleApplication1.cpp /link %linkerflags%
