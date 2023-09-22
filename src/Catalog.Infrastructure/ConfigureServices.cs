using Catalog.Application.Common.Interfaces;
using Catalog.Infrastructure.Persistence.AppDbContext;
using Catalog.Infrastructure.Persistence.Repositories.Products;
using Catalog.Infrastructure.Persistence.Repositories.Categories;
using Catalog.Application.Common.MessageProducer;
using Catalog.Infrastructure.MessageProducer;

namespace Catalog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IMessageProducerService, MessageProducerService>();

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}