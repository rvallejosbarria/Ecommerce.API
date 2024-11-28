namespace Ecommerce.Application;

using Microsoft.Extensions.DependencyInjection;
using Repositories;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<ICategoryRepository, CategoryRepository>();
        return services;
    }
}
