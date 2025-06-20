using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ofernandoavila.Mailman.Data.Mappings.License;

public class LicenseMapping : IEntityTypeConfiguration<Business.Models.License.License>
{
    public void Configure(EntityTypeBuilder<Business.Models.License.License> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Ignore(u => u.ValidationResult);

        builder.Property(u => u.ApplicationName)
                .IsRequired()
                .HasColumnType("varchar(100)");

        builder.Property(u => u.Hosts)
                .HasColumnType("varchar(900)");

        builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasColumnType("timestamp")
                .HasDefaultValueSql("timezone('America/Bahia', now())");

        builder.Property(u => u.ValidUntil)
                .HasColumnType("timestamp");

        builder.HasOne(u => u.User)
                .WithMany(r => r.Licenses)
                .HasForeignKey(u => u.UserId);

        builder.Property(u => u.Active)
                .IsRequired()
                .HasColumnType("boolean");

        builder.HasIndex(u => u.ApplicationName)
                .HasDatabaseName("IX_APPLICATIONNAME_TB_LICENSE")
                .IsUnique(false);

        builder.HasIndex(u => u.UserId)
                .HasDatabaseName("IX_USERID_TB_LICENSE")
                .IsUnique(false);

        builder.ToTable("TB_LICENSE");
    }
}