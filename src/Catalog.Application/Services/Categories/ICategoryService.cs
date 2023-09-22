using Catalog.Application.Common.Models;

namespace Catalog.Application.Services.Categories;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(CategoryDto category, CancellationToken cancellationToken = default);
    Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task AddAsync(CategoryDto category, CancellationToken cancellationToken = default);
}