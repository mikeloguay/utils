using System.ComponentModel.DataAnnotations;

namespace MyLib;
public record MyOptions
{
    public const string SectionName = "MyOptionsSection";

    public required string RequiredKeyWithDefaultValue { get; set; } = "MyDefaultValue";

    [Required]
    [MinLength(3)]
    public required string RequiredKey { get; set; }

    public string? OptionalKey { get; set; }
}