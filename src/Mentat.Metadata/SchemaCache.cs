using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Schema;

namespace Mentat.Metadata;

public class SchemaCache(IMemoryCache memoryCache) : ISchemaCache
{
    private readonly IMemoryCache _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

    private static readonly JsonSchemaExporterOptions _options = new()
    {
        TransformSchemaNode = (context, node) =>
        {
            var provider = context.PropertyInfo?.AttributeProvider;
            var description = provider?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .OfType<DescriptionAttribute>()
                .FirstOrDefault()?.Description;
            if (description != null)
                node["description"] = description;
            return node;
        },
        TreatNullObliviousAsNonNullable = true,
    };

    public async Task<JsonNode?> GetAsync<T>()
    {
        return await _memoryCache.GetOrCreateAsync(typeof(T), entry =>
        {
            return Task.FromResult(JsonSerializerOptions.Default.GetJsonSchemaAsNode(typeof(T), _options));
        });
    }
}