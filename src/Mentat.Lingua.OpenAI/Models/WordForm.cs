using Mentat.Lingua.OpenAI.Metadata;
using System.ComponentModel;

namespace Mentat.Lingua.OpenAI.Models;

[Description("Specific grammatical form of a given lexeme")]
public class WordForm
{
    [Description("Word form text")]
    public required string Text { get; set; }

    [Description("Natural-language expression of a full grammatical meaning of the word form")]
    [ChatRequestGuideline("express the grammatical meaning of each word form as a natural-language phrase or sentence fragment")]
    [ChatRequestGuideline("adapt the gloss to the part of speech: use noun phrases for nouns, simple verb clauses for verbs, and appropriate phrasing for other categories")]
    [ChatRequestGuideline("use the lemma where necessary to form a natural gloss")]
    [ChatRequestGuideline("do not use grammatical labels, case names, or linguistic jargon")]
    [ChatRequestGuideline("do not include parentheses or abstract role names")]
    public string? Gloss { get; set; }

    [Description("Optional additional lexico-semantic clarification")]
    public string? Meaning { get; set; }

    [Description("Formal grammatical categories for the word form")]
    [ChatRequestGuideline("do not include parentheses or abstract role names")]
    public required string[] Categories { get; set; }
}