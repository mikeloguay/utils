using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MyLib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMyServiceFromLib(this IServiceCollection services,
        Action<MyOptions>? options = null)
    {
        services.AddOptions<MyOptions>()
            .BindConfiguration(MyOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        if (options is not null) services.Configure(options);

        services.AddSingleton<IMyService, MyService>();
        return services;
    }
}