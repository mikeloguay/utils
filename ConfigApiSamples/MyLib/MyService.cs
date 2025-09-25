using Microsoft.Extensions.Options;

namespace MyLib;

public interface IMyService
{
    string PrintOptions();
}

public class MyService(IOptions<MyOptions> options) : IMyService
{
    private readonly MyOptions _options = options.Value;

    public string PrintOptions() => $"Options:\n" +
               "------------------------------------\n" +
               $"RequiredKeyWithDefaultValue: {_options.RequiredKeyWithDefaultValue}\n" +
               $"RequiredKey: {_options.RequiredKey}\n" +
               $"OptionalKey: {_options.OptionalKey ?? "null"}";
}