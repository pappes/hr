rem create library project containg the required source
rem based on instructions from http://asp.net-hacker.rocks/2017/03/31/unit-testing-with-dotnetcore.html
rem more info https://xunit.github.io/docs/comparisons.html
md %1
pushd %1
    dotnet new sln
    md prj
        pushd prj
        dotnet new classlib
REM         copy ..\..\..\simple.cs Class1.cs
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

    code prj\Class1.cs
    code lib.Xunit\UnitTest1.cs
popd
