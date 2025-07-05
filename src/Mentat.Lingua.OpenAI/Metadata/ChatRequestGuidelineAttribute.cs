namespace Mentat.Lingua.OpenAI.Metadata;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property, AllowMultiple = true)]
public class ChatRequestGuidelineAttribute(string details) : Attribute
{
    public string Details { get; set; } = details;
}