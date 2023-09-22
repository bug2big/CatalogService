using Catalog.Application.Common.Interfaces;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.AppDbContext;

namespace Catalog.Infrastructure.Persistence.Repositories.Categories;
public class CategoryRepository : BaseReporitory<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {

    }
}
