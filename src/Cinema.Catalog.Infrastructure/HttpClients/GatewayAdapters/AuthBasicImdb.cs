
using Cinema.Catalog.Domain.Shared;
using Microsoft.Extensions.Options;

namespace Cinema.Catalog.Infrastructure.HttpClients.GatewayAdapters;

class AuthBasicImdb(IOptions<TmdbApiOptions> options) : GatewayAdapterBase
{
    protected override HttpClient? Client { get; set; } = null;
    private readonly TmdbApiOptions _tmdbApiOptions = options.Value;

    public override Task Authenticate(HttpRequestMessage requestMessage)
    {
        requestMessage.Headers.TryAddWithoutValidation("Accept", "application/json");
        requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _tmdbApiOptions.AuthToken);
        return Task.CompletedTask;
    }
}
