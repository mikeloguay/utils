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
            //.ValidateDataAnnotations()
            .ValidateOnStart();

        if (options is not null) services.Configure(options);

        services.AddSingleton<IMyService, MyService>();
        return services;
    }
}