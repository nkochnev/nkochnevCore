using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NkochnevCore.Infrastructure.Domain;

namespace NkochnevCore.Infrastructure.Mapping
{
	public class ArticleDomainMap : IEntityTypeConfiguration<ArticleDomain>
	{
		public void Configure(EntityTypeBuilder<ArticleDomain> builder)
		{
			builder.ToTable("Articles");
			builder.HasKey(x => x.Key);
			builder.Property(x => x.Key)
				.HasMaxLength(100);
			builder.Property(x => x.Title).HasMaxLength(1000);
			builder.Property(x => x.Content);
			builder.Property(x => x.Created).IsRequired();

			builder.Property(x => x.SeoKeyWords).HasMaxLength(1000);
			builder.Property(x => x.SeoDescription).HasMaxLength(1000);
		}
	}
}