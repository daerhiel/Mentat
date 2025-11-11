using Mentat.Lingua.OpenAI.Metadata;
using Mentat.Lingua.OpenAI.Models;
using Mentat.Metadata;
using OpenAI.Chat;
using System.Text;
using System.Text.Json;

namespace Mentat.Lingua.OpenAI;

public class LexiconMediator(ChatClient chatClient, ISchemaCache schemaCache) : ILexiconMediator
{
    private readonly ChatClient _chatClient = chatClient ?? throw new ArgumentNullException(nameof(chatClient));
    private readonly ISchemaCache _schemaCache = schemaCache ?? throw new ArgumentNullException(nameof(schemaCache));

    public async Task<Lexeme> GetLexemeAsync(string word, CancellationToken cancellationToken = default)
    {
        var request = ChatRequestBuilder.GetRequest<Lexeme>(word, "Hungarian", "Russian");

        var schema = await _schemaCache.GetAsync<Lexeme>();
        var options = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "lexical_analysis",
                jsonSchemaFormatDescription: "Lexical Analysis",
                jsonSchema: BinaryData.FromObjectAsJson(schema)
            )
        };

        var stream = new StringBuilder();
        await foreach (var result in _chatClient.CompleteChatStreamingAsync([request], options, cancellationToken))
        {
            foreach (var message in result.ContentUpdate)
            {
                stream.Append(message.Text);
            }
        }

        return JsonSerializer.Deserialize<Lexeme>(stream.ToString()) ?? throw new InvalidDataException();
    }

    public async Task<Sentence> GetSyntaxAsync(string text, CancellationToken cancellationToken = default)
    {
        var request = ChatRequestBuilder.GetRequest<Sentence>(text, "Hungarian", "Russian");

        var schema = await _schemaCache.GetAsync<Sentence>();
        var options = new ChatCompletionOptions
        {
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "syntax_analysis",
                jsonSchemaFormatDescription: "Syntax Analysis",
                jsonSchema: BinaryData.FromObjectAsJson(schema)
            )
        };

        var stream = new StringBuilder();
        await foreach (var result in _chatClient.CompleteChatStreamingAsync([request], options, cancellationToken))
        {
            foreach (var message in result.ContentUpdate)
            {
                stream.Append(message.Text);
            }
        }

        return JsonSerializer.Deserialize<Sentence>(stream.ToString()) ?? throw new InvalidDataException();
    }
}