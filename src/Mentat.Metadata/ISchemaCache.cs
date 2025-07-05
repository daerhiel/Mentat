using System.Text.Json.Nodes;

namespace Mentat.Metadata;

public interface ISchemaCache
{
    Task<JsonNode?> GetAsync<T>();
}