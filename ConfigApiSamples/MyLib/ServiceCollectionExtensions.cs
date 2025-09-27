using Microsoft.Extensions.DependencyInjection;

namespace MyLib;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLibMainOptions(this IServiceCollection services,
        Action<MainOptions>? options = null)
    {
        services.AddOptions<MainOptions>()
            .BindConfiguration(MainOptions.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        if (options is not null) services.Configure(options);

        services.AddSingleton<IMyService, MyService>();
        return services;
    }

    public static IServiceCollection AddLibSecondOptions(this IServiceCollection services,
        Action<SecondOptions>? options = null)
    {
        services.AddOptions<SecondOptions>()
            .BindConfiguration(SecondOptions.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        if (options is not null) services.Configure(options);

        services.AddSingleton<IMyService, MyService>();
        return services;
    }
}