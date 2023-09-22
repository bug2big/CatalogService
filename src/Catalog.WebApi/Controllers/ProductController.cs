using Catalog.Application.Common.ApiModels;
using Catalog.Application.Common.Identity;
using Catalog.Application.Common.MessageProducer;
using Catalog.Application.Common.Models;
using Catalog.Application.Services.Products;
using Catalog.WebApi.Routing;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.WebApi.Controllers;

[ApiController]
[Route(Routes.ProductController.Endpoint)]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMessageProducerService _messageProducerService;

    public ProductController(
        IProductService productService,
        IMessageProducerService messageProducerService)
    {
        _productService = productService;
        _messageProducerService = messageProducerService;
    }

    [Authorize(Roles = $"{IdentityData.ManagerRoleClaimValue}, {IdentityData.BuyerRoleClaimValue}")]
    [HttpGet]
    public async Task<IActionResult> GetProducts(
        [FromQuery] FilterProductsWithPagination filterProductsWithPagination,
        CancellationToken cancellationToken = default)
    {
        var productsWithPagination = await _productService.GetProductsWithPaginationAsync(
            filterProductsWithPagination,
            cancellationToken);
        return Ok(productsWithPagination);
    }

    [Authorize(Roles = IdentityData.ManagerRoleClaimValue)]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] ProductDto productDto,
        CancellationToken cancellationToken = default)
    {
        await _productService.UpdateAsync(productDto, cancellationToken);
        var message = new UpdateProductModel
        {
            Id = productDto.Id,
            Name = productDto.Name,
            Description = productDto.Description,
            Amount = productDto.Amount,
            Image = productDto.Image,
            Price = productDto.Price
        };
        _messageProducerService.SendMessage(message);

        return Ok();
    }

    [Authorize(Roles = IdentityData.ManagerRoleClaimValue)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        ProductDto itemDto,
        CancellationToken cancellationToken = default)
    {
        await _productService.AddAsync(itemDto, cancellationToken);
        return Ok();
    }
    [Authorize(Roles = IdentityData.ManagerRoleClaimValue)]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        await _productService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
}