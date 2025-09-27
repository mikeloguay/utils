using System.ComponentModel.DataAnnotations;

namespace MyLib;
public record MyOptions
{
    public required string RequiredKeyWithDefaultValue { get; set; } = "MyDefaultValue";

    [MinLength(3)]
    public required string RequiredKey { get; set; }
    public string? OptionalKey { get; set; }
}