using Mentat.Lingua.OpenAI;
using Mentat.Lingua.OpenAI.Models;
using Mentat.Server.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Mentat.Server.Controllers;

/// <summary>
/// Provides endpoints for accessing and managing lexico-grammatical context.
/// </summary>
/// <param name="lexicon">The mediator for lexicon operations.</param>
/// <param name="cache">The memory cache for storing lexicon data.</param>
[ApiController]
[Route("api/[controller]")]
public class LexiconController(ILexiconMediator lexicon, IMemoryCache cache) : ControllerBase
{
    private readonly ILexiconMediator _lexicon = lexicon ?? throw new ArgumentNullException(nameof(lexicon));
    private readonly IMemoryCache _cache = cache ?? throw new ArgumentNullException(nameof(cache));

    /// <summary>
    /// Gets the lexeme for a given word.
    /// </summary>
    /// <param name="word">The word to analyze.</param>
    /// <param name="token">Cancellation token for the operation.</param>
    /// <returns>A <see cref="Lexeme"/> object containing the lexeme information.</returns>
    [HttpGet("lexeme")]
    public async Task<Lexeme> GetLexeme([FromQuery] string word, CancellationToken token)
    {
        return await _cache.GetOrCreateAsync(word, async entry =>
        {
            return await _lexicon.GetLexemeAsync(word, token).ConfigureAwait(false);
        }) ?? throw new InvalidDataException();
    }

    /// <summary>
    /// Gets the syntax analysis for a given text.
    /// </summary>
    /// <param name="input">The input containing the text to analyze.</param>
    /// <param name="token">Cancellation token for the operation.</param>
    /// <returns>An array of <see cref="Sentence"/> objects containing the syntax analysis.</returns>
    [HttpPost("syntax")]
    public async Task<Sentence[]> GetSyntax([FromBody] Input input, CancellationToken token)
    {
        return await _lexicon.GetSyntaxAsync(input.Text, token).ConfigureAwait(false);
        //return await _cache.GetOrCreateAsync(text, async entry =>
        //{
        //}) ?? throw new InvalidDataException();
    }
}