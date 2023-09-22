using Catalog.Application.Common.Models;
using Catalog.Domain.Entities;

namespace Catalog.Application.Common.Mappers;

[Mapper]
public partial class ProductMapper
{
    public partial ProductDto MapToProductDto(Product product);

    public partial Product MapToProduct(ProductDto product);
}
