using Microsoft.Extensions.Options;

namespace MyLib;

public interface IMyService
{
    string PrintOptions();
}

public class MyService(IOptions<MainOptions> mainOptions, IOptions<SecondOptions> secondOptions) : IMyService
{
    private readonly MainOptions _mainOptions = mainOptions.Value;
    private readonly SecondOptions _secondOptions = secondOptions.Value;

    public string PrintOptions() => $"MainOptions:\n" +
               "------------------------------------\n" +
               $"RequiredKeyWithDefaultValue: {_mainOptions.RequiredKeyWithDefaultValue}\n" +
               $"RequiredKey: {_mainOptions.RequiredKey}\n" +
               $"OptionalKey: {_mainOptions.OptionalKey ?? "null"}\n" +
               "------------------------------------\n" +
               $"RequiredKeyWithDefaultValue: {_secondOptions.SecondRequiredKeyWithDefaultValue}\n" +
               $"RequiredKey: {_secondOptions.SecondRequiredKey}\n" +
               $"OptionalKey: {_secondOptions.SecondOptionalKey ?? "null"}"
               ;

}