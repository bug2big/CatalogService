namespace Catalog.Domain.Common;

public record BaseEntity
{
    public Guid Id { get; init; }
}