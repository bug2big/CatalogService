using Catalog.Application.Common.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.AppDbContext;

namespace Catalog.Infrastructure.Persistence.Repositories.Products;

public class ProductRepository : BaseReporitory<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {

    }
}
