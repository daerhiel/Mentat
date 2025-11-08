using Mentat.Lingua.OpenAI.Models;

namespace Mentat.Lingua.OpenAI;

public interface ILexiconMediator
{
    Task<Lexeme> GetLexemeAsync(string word, CancellationToken cancellationToken = default);

    Task<Sentence[]> GetSyntaxAsync(string text, CancellationToken cancellationToken = default);
}