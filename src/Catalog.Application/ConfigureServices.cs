using Catalog.Application.Common.Mappers;
using Catalog.Application.Services.Categories;
using Catalog.Application.Services.Products;

namespace Catalog.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<CategoryMapper>();
        services.AddScoped<ProductMapper>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        return services;
    }
}