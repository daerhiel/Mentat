using Mentat.Metadata;
using Microsoft.Extensions.DependencyInjection;
using OpenAI.Chat;
using System.ClientModel;

namespace Mentat.Lingua.OpenAI;

public static class Startup
{
    public static IServiceCollection AddLinguaOpenAI(this IServiceCollection services)
    {
        services.AddSchemaCache();
        services.AddScoped(provider =>
        {
            var credentials = new ApiKeyCredential(Environment.GetEnvironmentVariable("OPENAI_API_KEY")!);
            return new ChatClient("gpt-4.1", credentials);
        });
        services.AddScoped<ILexiconMediator, LexiconMediator>();
        return services;
    }
}