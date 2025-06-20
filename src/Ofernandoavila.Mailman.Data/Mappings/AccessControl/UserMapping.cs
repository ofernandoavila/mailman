using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofernandoavila.Mailman.Business.Models.AccessControl;

namespace Ofernandoavila.Mailman.Data.Mappings.AccessControl;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey( u => u.Id);

        builder.Ignore( u => u.SessionId);
        builder.Ignore( u => u.LicenseId);
        builder.Ignore( u => u.ValidationResult);

        builder.Property( u => u.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

        builder.Property( u => u.Email )
                .IsRequired()
                .HasColumnType("varchar(100)");

        builder.Property( u => u.Password )
                .IsRequired()
                .HasColumnType("varchar(64)");
        
        builder.Property( u => u.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp")
                .HasDefaultValueSql("timezone('America/Bahia', now())");

        builder.Property( u => u.FirstAccess )
                .IsRequired()
                .HasColumnType("boolean")
                .HasDefaultValue(true);
        
        builder.Property( u => u.SignUpEmailSent )
                .IsRequired()
                .HasColumnType("boolean");

        builder.Property( u => u.PasswordResetEmailSent )
                .IsRequired()
                .HasColumnType("boolean");

        builder.HasOne( u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey( u => u.RoleId);

        builder.HasMany(u => u.Licenses)
                .WithOne(r => r.User)
                .HasForeignKey(u => u.UserId);

        builder.Property(u => u.Active)
                .IsRequired()
                .HasColumnType("boolean");

        builder.HasIndex( u => u.Email)
                .HasDatabaseName("IX_EMAIL_TB_USER")
                .IsUnique();

        builder.HasIndex( u => u.Name)
                .HasDatabaseName("IX_NAME_TB_USER")
                .IsUnique(false);

        builder.HasIndex( u => u.RoleId)
                .HasDatabaseName("IX_ROLEID_TB_USER")
                .IsUnique(false);

        builder.ToTable("TB_USER");
    }
}