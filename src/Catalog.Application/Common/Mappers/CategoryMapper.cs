using Catalog.Application.Common.Models;
using Catalog.Domain.Entities;

namespace Catalog.Application.Common.Mappers;

[Mapper]
public partial class CategoryMapper
{
    public partial Category MapToCategory(CategoryDto categoryDto);

    public partial CategoryDto MapToCategoryDto(Category category);
}
