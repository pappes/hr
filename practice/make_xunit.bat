rem create library project containg the required source
rem based on instructions from http://asp.net-hacker.rocks/2017/03/31/unit-testing-with-dotnetcore.html
rem more info https://xunit.github.io/docs/comparisons.html
md %1
cd %1
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
            dotnet add reference ..\..\..\..\..\lib\TestHelper\TestHelper.csproj
        popd
        dotnet sln add lib.Xunit\lib.Xunit.csproj

        dotnet restore
        dotnet build

        dotnet test lib.Xunit\lib.Xunit.csproj
    popd
popd

