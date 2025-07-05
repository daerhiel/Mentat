using Microsoft.Extensions.DependencyInjection;

namespace Mentat.Metadata;

public static class Startup
{
    public static IServiceCollection AddSchemaCache(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<ISchemaCache, SchemaCache>();
        return services;
    }
}