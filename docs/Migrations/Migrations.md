### 📌 Managing Migrations

### Add a Migration

To create a new migration:

```sh
dotnet ef migrations add <MIGRATION NAME> --project ../Ofernandoavila.Mailman.Data/Ofernandoavila.Mailman.Data.csproj --startup-project . --context AppDbContext
```

### Remove Last Migration

If you need to undo the last migration:

```sh
dotnet ef migrations remove --project ../Ofernandoavila.Mailman.Data/Ofernandoavila.Mailman.Data.csproj --startup-project . --context AppDbContext
```

### Apply Migrations

To apply migrations and update the database:

```sh
dotnet ef database update --project ../Ofernandoavila.Mailman.Data/Ofernandoavila.Mailman.Data.csproj --startup-project . --context AppDbContext
```