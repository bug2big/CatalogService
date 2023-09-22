using Catalog.Application.Common.Interfaces;
using Catalog.Application.Common.Mappers;
using Catalog.Application.Common.Models;
using Catalog.Application.Services.Categories;
using Catalog.Domain.Entities;

namespace Catalog.Tests.Services;

[TestClass]
public class CategoryServiceTests
{
    private CategoryService _categoryService = null!;
    private Mock<ICategoryRepository> _categoryRepository = null!;
    private Mock<CategoryMapper> _categoryMapper = null!;

    [TestInitialize]
    public void Initialize()
    {
        _categoryRepository = new Mock<ICategoryRepository>();
        _categoryMapper = new Mock<CategoryMapper>();
        _categoryService = new CategoryService(_categoryRepository.Object, _categoryMapper.Object);
    }

    [TestMethod]
    public async Task AddAsync_WhenEntityIsValid_ShouldCall()
    {
        // Arange
        var tokenNone = CancellationToken.None;
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        };
        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = "Test"
        };

        // Act
        await _categoryService.AddAsync(categoryDto, tokenNone);

        // Assert
        _categoryRepository.Verify(r => r.AddAsync(category, tokenNone), Times.Once);
    }

    [TestMethod]
    public async Task AddAsync_WhenRepositoryThrowException_ShouldThrow()
    {
        // Arrange
        var tokenNone = CancellationToken.None;
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        };
        var categoryDto = new CategoryDto
        {
            Id = category.Id,
            Name = "Test"
        };
        var customException = new Exception("custom exception");
        _categoryRepository.Setup(x => x.AddAsync(category, tokenNone))
            .Throws(customException);

        // Act & Assert
        await _categoryService.Invoking(s => s.AddAsync(categoryDto, tokenNone))
            .Should()
            .ThrowAsync<Exception>()
            .WithMessage(customException.Message);
    }

    [TestMethod]
    public async Task DeleteAsync()
    {
        // Arrange
        var tokenNone = CancellationToken.None;
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        };
        _categoryRepository.Setup(x => x.GetByIdAsync(category.Id, tokenNone))
            .ReturnsAsync(category);

        // Act
        await _categoryService.Invoking(s => s.DeleteAsync(category.Id, tokenNone))
            .Should()
            .NotThrowAsync();

        // Assert
        _categoryRepository.Verify(x => x.DeleteAsync(category, tokenNone), Times.Once);
        _categoryRepository.Verify(x => x.GetByIdAsync(category.Id, tokenNone), Times.Once);
    }
}