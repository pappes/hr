rem create library project containg the required source
rem based on instructions from http://asp.net-hacker.rocks/2017/03/31/unit-testing-with-dotnetcore.html
rem more info https://xunit.github.io/docs/comparisons.html
md project
pushd project
    md library
    pushd library
        dotnet new sln
        md prj
            pushd prj
            dotnet new classlib
            copy ..\..\..\simple.cs Class1.cs
        popd
        dotnet sln add prj\prj.csproj

        dotnet restore
        dotnet build

        rem create Xunit project and reference the library project
        md lib.Xunit
        pushd lib.Xunit
            dotnet new xunit
            dotnet add reference ..\prj\prj.csproj
        popd
        dotnet sln add lib.Xunit\lib.Xunit.csproj

        dotnet restore
        dotnet build

        dotnet test lib.Xunit\lib.Xunit.csproj


        rem create MSTest project and reference the library project
        md lib.MsTest
        pushd lib.MsTest
            dotnet new mstest
            dotnet add reference ..\prj\prj.csproj
        popd
        dotnet sln add lib.MsTest\lib.MsTest.csproj

        dotnet restore
        dotnet build

        dotnet test lib.MsTest\lib.MsTest.csproj

REM 
REM         rem create Nunit project as an executable console app and reference the library project
REM         md lib.Nunit
REM         pushd lib.Nunit
REM             dotnet new console
REM             dotnet add package Nunit
REM             dotnet add package NUnitLite
REM             dotnet add reference ..\prj\prj.csproj
REM         popd
REM 
REM         dotnet restore
REM         dotnet build
REM 
REM         dotnet run -p lib.Nunit\lib.Nunit.csproj

        rem create Nunit project (Rob Prouse) https://github.com/nunit/docs/wiki/.NET-Core-and-.NET-Standard
        md lib.Nunit
        pushd lib.Nunit
            dotnet new -i NUnit3.DotNetNew.Template 
            dotnet new nunit
            dotnet add reference ..\prj\prj.csproj
        popd

        dotnet restore
        dotnet build

        dotnet test lib.Nunit\lib.Nunit.csproj
    popd
popd

