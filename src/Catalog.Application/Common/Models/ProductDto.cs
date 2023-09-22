namespace Catalog.Application.Common.Models;

public record ProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Image { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
    public Guid CategoryId { get; set; }
}