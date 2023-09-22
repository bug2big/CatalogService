using Catalog.Domain.Entities;

namespace Catalog.Domain.Common.EntityConfigurations;

public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.ParentCategory);
        builder.HasMany(x => x.Products)
            .WithOne(x => x.Category)
            .OnDelete(DeleteBehavior.Cascade);
    }
}