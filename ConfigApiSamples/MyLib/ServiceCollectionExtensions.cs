using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MyLib;
public static class ServiceCollectionExtensions
{
    public static IMyLibConfigurator AddLibMainOptions(this IServiceCollection services,
        Action<MainOptions>? options = null)
    {
        OptionsBuilder<MainOptions> optionsBuilder = services.AddOptions<MainOptions>()
            .BindConfiguration(MainOptions.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        if (options is not null) optionsBuilder.Configure(options);

        return new MyLibConfigurator(services);
    }
}

public interface IMyLibConfigurator
{
    IServiceCollection AddLibSecondOptions(Action<SecondOptions>? options = null);
}

internal class MyLibConfigurator(IServiceCollection services) : IMyLibConfigurator
{
    private readonly IServiceCollection _services = services;

    public IServiceCollection AddLibSecondOptions(
        Action<SecondOptions>? options = null)
    {
        OptionsBuilder<SecondOptions> optionsBuilder = _services.AddOptions<SecondOptions>()
            .BindConfiguration(SecondOptions.SECTION_NAME)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        if (options is not null) optionsBuilder.Configure(options);

        _services.AddSingleton<IMyService, MyService>();

        return _services;
    }
}