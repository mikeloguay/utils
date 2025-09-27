using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MyLib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMyServiceFromLib(this IServiceCollection services,
        IConfigurationSection configSection,
        Action<MyOptions>? options = null)
    {
        services.AddOptions<MyOptions>()
            .Bind(configSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IMyService, MyService>();
        return services;
    }
}