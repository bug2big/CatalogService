using Catalog.Application.Common.Exceptions;
using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Mappers;
using Catalog.Application.Common.Models;
using Catalog.Domain.Entities;

namespace Catalog.Application.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly CategoryMapper _categoryMapper;

    public CategoryService(
        ICategoryRepository categoryRepository,
        CategoryMapper categoryMapper)
    {
        _categoryRepository = categoryRepository;
        _categoryMapper = categoryMapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return (await _categoryRepository.GetAllAsync(cancellationToken))
            .Select(_categoryMapper.MapToCategoryDto);
    }

    public async Task UpdateAsync(CategoryDto categoryDto, CancellationToken cancellationToken = default)
    {
        await _categoryRepository.UpdateAsync(_categoryMapper.MapToCategory(categoryDto), cancellationToken);
    }

    public async Task DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId, cancellationToken);

        if (category == null)
        {
            throw new NotFoundException(nameof(Category), categoryId);
        }

        await _categoryRepository.DeleteAsync(category, cancellationToken);
    }

    public async Task AddAsync(CategoryDto categoryDto, CancellationToken cancellationToken = default)
    {
        await _categoryRepository.AddAsync(_categoryMapper.MapToCategory(categoryDto), cancellationToken);
    }
}