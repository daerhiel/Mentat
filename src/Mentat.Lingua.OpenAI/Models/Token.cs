using System.ComponentModel;

namespace Mentat.Lingua.OpenAI.Models;

[Description("Token, a single unit of text, such as a word or punctuation mark, in a sentence")]
public class Token
{
    [Description("Original text of a token in the sentence")]
    public required string Text { get; set; }

    [Description("Correct form of a token")]
    public required string Form { get; set; }

    [Description("Lemma form of a token")]
    public required string Lemma { get; set; }

    [Description("Part of speech of a token")]
    public required string PartOfSpeech { get; set; }
}
