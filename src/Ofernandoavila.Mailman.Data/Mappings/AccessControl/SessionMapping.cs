using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ofernandoavila.Mailman.Business.Models.AccessControl;

namespace Ofernandoavila.Mailman.Data.Mappings.AccessControl;

public class SessionMapping : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(s => s.Id );

        builder.Ignore( s => s.ValidationResult );

        builder.HasOne( s => s.User )
                .WithMany( s => s.Session );

        builder.Property( s => s.UserAgent )
                .IsRequired()
                .HasColumnType("varchar(500)");

        builder.Property( s => s.Token )
                .IsRequired()
                .HasColumnType("varchar(1000)");

        builder.Property( s => s.RefreshToken )
                .IsRequired()
                .HasColumnType("varchar(100)");

        builder.Property( s => s.CreationTime )
                .IsRequired()
                .HasColumnType("timestamp")
                .HasDefaultValueSql("timezone('America/Bahia', now())");

        builder.Property( s => s.ExpirationTime )
                .IsRequired()
                .HasColumnType("timestamp");

        builder.ToTable("TB_SESSION");
    }
}