using System.ComponentModel.DataAnnotations;
using Catalog.Domain.Common;

namespace Catalog.Domain.Entities;

public record Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Image { get; set; }
    
    public double Price { get; set; }

    [Range(1, int.MaxValue)]
    public int Amount { get; set; }
    
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
}