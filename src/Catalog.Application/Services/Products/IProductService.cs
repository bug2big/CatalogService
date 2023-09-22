using Catalog.Application.Common.ApiModels;
using Catalog.Application.Common.Models;

namespace Catalog.Application.Services.Products;

public interface IProductService
{
    Task<PaginatedList<ProductDto>> GetProductsWithPaginationAsync(
        FilterProductsWithPagination filterProductsWithPagination, 
        CancellationToken cancellationToken = default);
    Task UpdateAsync(ProductDto productDto, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid productId, CancellationToken cancellationToken = default);
    Task AddAsync(ProductDto productDto, CancellationToken cancellationToken = default);
}