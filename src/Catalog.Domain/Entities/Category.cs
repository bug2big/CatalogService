using Catalog.Domain.Common;

namespace Catalog.Domain.Entities;

public record Category : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Image { get; set; }
    public Guid? ParentCategoryId { get; set; }

    public virtual Category? ParentCategory { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}