using System.ComponentModel;

namespace Mentat.Lingua.OpenAI.Models;

[Description("Sentence, a sequence of words that expresses a complete thought or idea")]
public class Sentence
{
    [Description("Source text of the sentence.")]
    public required string Text { get; set; }

    [Description("Language of the sentence")]
    public required string Language { get; set; }

    [Description("Grammatical type of the sentence")]
    public required string Type { get; set; }

    [Description("List of tokens in the sentence, each token is a word or punctuation mark")]
    public List<Token> Tokens { get; } = [];
}