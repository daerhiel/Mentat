using Mentat.Lingua.OpenAI.Metadata;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Mentat.Lingua.OpenAI.Models;

[Description("Abstract lexical unit, having all forms of a given word")]
public class Lexeme
{
    [Description("Language of a lexeme")]
    public required string Language { get; set; }

    [Description("Lemma or citation form of the lexeme")]
    public required string Lemma { get; set; }

    [Description("Base grammatical category, only lexical part of speech")]
    [ChatRequestGuideline("use lower case for grammatical categories")]
    [ChatRequestGuideline("use the full formal name of a grammatical category, not abbreviations or short forms")]
    public required string Category { get; set; }

    [Description("Lexico-semantic or ontological properties")]
    public List<string> Properties { get; } = [];

    [Description("Grammatically distinct forms of the lexeme")]
    [JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
    public List<WordForm> Forms { get; } = [];
}