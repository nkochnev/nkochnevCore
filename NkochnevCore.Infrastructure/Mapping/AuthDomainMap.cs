using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NkochnevCore.Infrastructure.Domain;

namespace NkochnevCore.Infrastructure.Mapping
{
    public class AuthDomainMap : IEntityTypeConfiguration<AuthDomain>
    {
        public void Configure(EntityTypeBuilder<AuthDomain> builder)
        {
            builder.ToTable("Auth");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.PassHash)
                .HasMaxLength(500);
        }
    }
}