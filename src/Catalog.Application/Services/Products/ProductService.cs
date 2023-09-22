using Catalog.Application.Common.ApiModels;
using Catalog.Application.Common.Exceptions;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Mappers;
using Catalog.Application.Common.Models;

namespace Catalog.Application.Services.Products;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly ProductMapper _productMapper;

    public ProductService(
        IProductRepository productRepository,
        ProductMapper productMapper)
    {
        _productRepository = productRepository;
        _productMapper = productMapper;
    }

    public async Task<PaginatedList<ProductDto>> GetProductsWithPaginationAsync(
        FilterProductsWithPagination filterProductsWithPagination,
        CancellationToken cancellationToken = default)
    {
        var products = (await _productRepository.GetAllAsync(cancellationToken))
            .Where(p =>
                !filterProductsWithPagination.CategoryId.HasValue
                || p.CategoryId == filterProductsWithPagination.CategoryId)
            .Select(_productMapper.MapToProductDto)
            .AsQueryable();

        return PaginatedList<ProductDto>.Create(
            products,
            filterProductsWithPagination.PageNumber,
            filterProductsWithPagination.PageSize);
    }

    public async Task UpdateAsync(ProductDto productDto, CancellationToken cancellationToken = default)
    {
        await _productRepository.UpdateAsync(_productMapper.MapToProduct(productDto), cancellationToken);
    }

    public async Task DeleteAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        if (product != null)
        {
            await _productRepository.DeleteAsync(product, cancellationToken);
        }

        throw new NotFoundException(nameof(productId), productId);
    }

    public async Task AddAsync(ProductDto productDto, CancellationToken cancellationToken = default)
    {
        await _productRepository.AddAsync(_productMapper.MapToProduct(productDto), cancellationToken);
    }
}