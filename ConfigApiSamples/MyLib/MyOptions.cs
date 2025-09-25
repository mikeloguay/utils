namespace MyLib;
public record MyOptions
{
    public required string RequiredKeyWithDefaultValue { get; set; } = "MyDefaultValue";
    public required string RequiredKey { get; set; }
    public string? OptionalKey { get; set; }
}