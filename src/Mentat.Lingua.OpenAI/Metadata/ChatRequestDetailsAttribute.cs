namespace Mentat.Lingua.OpenAI.Metadata;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property)]
public class ChatRequestDetailsAttribute(string value) : Attribute
{
    public string Value { get; } = value;
}
