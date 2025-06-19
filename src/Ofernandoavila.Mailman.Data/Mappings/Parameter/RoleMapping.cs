using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofernandoavila.Mailman.Business.Models.Parameter;

namespace Ofernandoavila.Mailman.Data.Mappings.Parameter;

public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey( r => r.Id );
        
        builder.Ignore( r => r.ValidationResult );
        builder.Ignore( r => r.UserId );

        builder.Property( r => r.Description )
                .IsRequired()
                .HasColumnType("varchar(50)");

        builder.Property( r => r.Active )
                .IsRequired()
                .HasColumnType("int");

        builder.HasIndex( r => r.Description )
                .HasDatabaseName("IX_DESCRIPTION_TB_ROLE")
                .IsUnique();

        builder.ToTable("TB_ROLE");
    }
}