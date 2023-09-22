using Catalog.Domain.Entities;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations;

public class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Price)
            .IsRequired();
        builder.Property(x => x.Amount)
            .IsRequired();
    }
}