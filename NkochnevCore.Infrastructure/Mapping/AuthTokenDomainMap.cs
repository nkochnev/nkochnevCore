using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NkochnevCore.Infrastructure.Domain;

namespace NkochnevCore.Infrastructure.Mapping
{
    internal class AuthTokenDomainMap : IEntityTypeConfiguration<AuthTokenDomain>
    {
        public void Configure(EntityTypeBuilder<AuthTokenDomain> builder)
        {
            builder.ToTable("Tokens");
            builder.HasKey(x => x.RefreshToken);
            builder.Property(x => x.RefreshToken)
                .HasMaxLength(40);
            builder.Property(x => x.Expiresin).IsRequired();
            builder.Property(x => x.JwtToken).IsRequired();
            builder.Property(x => x.RefreshTokenValid).HasDefaultValue(true);
        }
    }
}