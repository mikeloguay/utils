using Microsoft.Extensions.DependencyInjection;

namespace MyLib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMyServiceFromLib(this IServiceCollection services)
    {
        services.AddSingleton<IMyService, MyService>();
        return services;
    }
}