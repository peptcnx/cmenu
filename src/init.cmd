dotnet new sln -n CMenu
dotnet new classlib -n CMenu
dotnet new console -n CMenu.Console
dotnet new xunit -n CMenu.Tests

dotnet sln CMenu.sln add CMenu\CMenu.csproj
dotnet sln CMenu.sln add CMenu.Console\CMenu.Console.csproj
dotnet sln CMenu.sln add CMenu.Tests\CMenu.Tests.csproj