namespace Catalog.Application.Common.Models;

public record CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Image { get; set; }
    public Guid? ParentCategoryId { get; set; }
}