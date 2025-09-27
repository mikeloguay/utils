using System.ComponentModel.DataAnnotations;

namespace MyLib;
public record MainOptions
{
    public const string SECTION_NAME = "MainOptionsSection";

    public required string RequiredKeyWithDefaultValue { get; set; } = "MyDefaultValue";

    [Required]
    [MinLength(3)]
    public required string RequiredKey { get; set; }

    public string? OptionalKey { get; set; }
}