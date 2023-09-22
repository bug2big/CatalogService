using Catalog.Application.Common.Identity;
using Catalog.Application.Common.Models;
using Catalog.Application.Services.Categories;
using Catalog.WebApi.Routing;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.WebApi.Controllers;

[ApiController]
[Route(Routes.CategoryController.Endpoint)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [Authorize(Roles = $"{IdentityData.ManagerRoleClaimValue}, {IdentityData.BuyerRoleClaimValue}")]
    [HttpGet(Routes.CategoryController.Action.GetAll)]
    public async Task<IActionResult> GetAllAsync()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories);
    }

    [Authorize(Roles = IdentityData.ManagerRoleClaimValue)]
    [HttpPut(Routes.CategoryController.Action.Update)]
    public async Task<IActionResult> UpdateAsync(CategoryDto categoryDto, CancellationToken cancellationToken = default)
    {
        await _categoryService.UpdateAsync(categoryDto, cancellationToken);
        return Ok();
    }

    [Authorize(Roles = IdentityData.ManagerRoleClaimValue)]
    [HttpPost(Routes.CategoryController.Action.Create)]
    public async Task<IActionResult> CreateAsync(CategoryDto categoryDto, CancellationToken cancellationToken = default)
    {
        await _categoryService.AddAsync(categoryDto, cancellationToken);
        return Ok();
    }

    [Authorize(Roles = IdentityData.ManagerRoleClaimValue)]
    [HttpDelete(Routes.CategoryController.Action.Delete)]
    public async Task<IActionResult> DeleteAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        await _categoryService.DeleteAsync(categoryId, cancellationToken);
        return Ok();
    }
}