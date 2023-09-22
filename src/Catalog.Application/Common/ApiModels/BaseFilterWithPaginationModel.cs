namespace Catalog.Application.Common.ApiModels;

public record BaseFilterWithPaginationModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
