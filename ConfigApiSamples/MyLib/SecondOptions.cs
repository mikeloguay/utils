using System.ComponentModel.DataAnnotations;

namespace MyLib;
public record SecondOptions
{
    public const string SECTION_NAME = "SecondOptionsSection";

    public required string SecondRequiredKeyWithDefaultValue { get; set; } = "MyDefaultValue";

    [Required]
    [MinLength(3)]
    public required string SecondRequiredKey { get; set; }

    public string? SecondOptionalKey { get; set; }
}