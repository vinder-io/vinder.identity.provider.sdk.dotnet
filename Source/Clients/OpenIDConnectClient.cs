namespace Vinder.IdentityProvider.Sdk.Clients;

public sealed class OpenIDConnectClient(HttpClient httpClient) : IOpenIDConnectClient
{
    public async Task<Result<ClientAuthenticationResult>> AuthenticateAsync(
        ClientAuthenticationCredentials credentials, CancellationToken cancellation = default)
    {
        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = credentials.ClientId,
            ["client_secret"] = credentials.ClientSecret,
            ["grant_type"] = credentials.GrantType,
        };

        var formContent = new FormUrlEncodedContent(parameters);

        var response = await httpClient.PostAsync("api/v1/openid/connect/token", formContent, cancellation);
        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<ClientAuthenticationResult>.Failure(error)
                : Result<ClientAuthenticationResult>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<ClientAuthenticationResult>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<ClientAuthenticationResult>.Success(result)
            : Result<ClientAuthenticationResult>.Failure(SdkErrors.DeserializationFailure);
    }
}