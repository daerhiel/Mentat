using Microsoft.EntityFrameworkCore.Design.Internal;
using System.Reflection;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Mentat.Lingua.OpenAI.Metadata;

public class ChatRequestBuilder
{
    public static JsonSerializerOptions DefaultOptions { get; } = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        TypeInfoResolver = new DefaultJsonTypeInfoResolver(),
        WriteIndented = true,
        IndentCharacter = ' ',
        IndentSize = 2
    };

#pragma warning disable EF1001 // Internal EF Core API usage.
    public static HumanizerPluralizer Plurals { get; } = new();
#pragma warning restore EF1001 // Internal EF Core API usage.

    private static void GetStructureRequirements(Type type, StringBuilder request)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(request);

        var guidelines = new Dictionary<string, HashSet<string>>();

        request.AppendLine("requirements:");
        GetTypeRequirements(type);

        request.AppendLine("guidelines:");
        foreach (var (guideline, details) in guidelines)
        {
            request.AppendLine($"- {guideline}:");
            foreach (var detail in details)
            {
                request.AppendLine($"  - {detail}");
            }
        }

        void GetTypeRequirements(Type type, int indent = 0)
        {
            var typeInfo = DefaultOptions.GetTypeInfo(type);
            var offset = new string(' ', indent * 2);
            request.AppendLine($"{offset}- {typeInfo.Type.Name}:");
            foreach (var property in typeInfo.Properties)
            {
                if (property.AttributeProvider is not ICustomAttributeProvider provider)
                    continue;

                var propertyName = property.Name;
                var test = new HashSet<string>();
                foreach (var attribute in provider.GetCustomAttributes(true))
                {
                    switch (attribute)
                    {
                        case ChatRequestDetailsAttribute details:
                            request.AppendLine($"{offset}  - {propertyName}: {details.Value}");
                            break; ;

                        case ChatRequestGuidelineAttribute { Details: string details }:
                            test.Add(details);
                            break; ;
                    }
                }
                if (test.Count > 0)
                {
#pragma warning disable EF1001 // Internal EF Core API usage.
                    var referenceName = Plurals.Singularize(propertyName);
#pragma warning restore EF1001 // Internal EF Core API usage.
                    if (!guidelines.TryGetValue(referenceName, out var set))
                    {
                        guidelines[referenceName] = set = [];
                    }
                    request.AppendLine($"{offset}  - {propertyName}: follow <{referenceName}> guidelines");
                    set.UnionWith(test);
                }

                var propertyTypeInfo = DefaultOptions.GetTypeInfo(property.PropertyType);
                if (propertyTypeInfo is { Kind: JsonTypeInfoKind.Enumerable or JsonTypeInfoKind.Dictionary, ElementType: Type elementType } &&
                    DefaultOptions.GetTypeInfo(elementType) is { Kind: JsonTypeInfoKind.Object })
                {
                    GetTypeRequirements(elementType, indent + 1);
                }
                if (propertyTypeInfo.Kind == JsonTypeInfoKind.Object)
                {
                    GetTypeRequirements(propertyTypeInfo.Type, indent + 1);
                }
            }
        }
    }

    public static string GetRequest<T>(string word, string sourceLanguage, string targetLanguage)
        where T : class => GetRequest(typeof(T), word, sourceLanguage, targetLanguage);

    public static string GetRequest(Type type, string word, string sourceLanguage, string targetLanguage)
    {
        ArgumentNullException.ThrowIfNull(type);
        ArgumentNullException.ThrowIfNull(word);
        ArgumentNullException.ThrowIfNull(sourceLanguage);
        ArgumentNullException.ThrowIfNull(targetLanguage);

        var request = new StringBuilder();
        request.AppendLine($"word: {word}")
            .AppendLine($"word language: {sourceLanguage}")
            .AppendLine($"gloss language: {targetLanguage}")
            .AppendLine($"request: lexical analysis")
            .AppendLine($"details: all word forms of the word for grammatical case only")
            .AppendLine("- follow the provided schema strictly")
            .AppendLine("- formatting must be uniform across languages and parts of speech");

        GetStructureRequirements(type, request);
        return request.ToString();
    }
}