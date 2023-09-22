namespace Catalog.Application.Common.ApiModels;

public record FilterProductsWithPagination : BaseFilterWithPaginationModel
{
    public Guid? CategoryId { get; set; }
}
