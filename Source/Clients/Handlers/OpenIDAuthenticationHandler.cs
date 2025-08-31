namespace Vinder.IdentityProvider.Sdk.Clients.Handlers;

public sealed class OpenIDAuthenticationHandler(IOpenIDConnectClient openIDClient, ClientCredentials credentials) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var clientCredentials = new ClientAuthenticationCredentials
        {
            ClientId = credentials.ClientId,
            ClientSecret = credentials.ClientSecret
        };

        var result = await openIDClient.AuthenticateAsync(clientCredentials, cancellationToken);
        if (result.IsFailure || result.Data is null)
        {
            throw new InvalidOperationException($"{result.Error.Code} - {result.Error.Description}");
        }

        var token = result.Data.AccessToken;

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        return await base.SendAsync(request, cancellationToken);
    }
}
