using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Ofernandoavila.Mailman.Business.Models.AccessControl;
using Ofernandoavila.Mailman.Business.Models.Parameter;

namespace Ofernandoavila.Mailman.Data.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Session> Session { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("en_US.utf8");

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string) && p.GetColumnType() is null)))
            property.SetColumnType("character varying(100)");

        foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.Name == "Code" || p.Name == "CreatedAt")))
            property.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

        Seed.SeedEntities(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }
}