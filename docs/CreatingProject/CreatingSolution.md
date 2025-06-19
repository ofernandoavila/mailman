# Creating a C# solution

```bash
dotnet new sln --output Ofernandoavila.Mailman.Api
```
# Creating the architecture

Creating the Api Layer
```bash
dotnet new webapi --output ./src/Ofernandoavila.Mailman.Api
```

Creating the Business Layer
```bash
dotnet new classlib --output ./src/Ofernandoavila.Mailman.Business
```

Creating the Data Layer
```bash
dotnet new classlib --output ./src/Ofernandoavila.Mailman.Data
```

# Configuring the project's solution
```bash
dotnet sln add ./src/Ofernandoavila.Mailman.Api/Ofernandoavila.Mailman.Api.csproj
dotnet sln add ./src/Ofernandoavila.Mailman.Business/Ofernandoavila.Mailman.Business.csproj
dotnet sln add ./src/Ofernandoavila.Mailman.Data/Ofernandoavila.Mailman.Data.csproj
```