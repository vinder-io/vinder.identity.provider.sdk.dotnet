namespace Vinder.IdentityProvider.Sdk.Contracts;

public interface IOpenIDConnectClient
{
    public Task<Result<ClientAuthenticationResult>> AuthenticateAsync(
        ClientAuthenticationCredentials credentials,
        CancellationToken cancellation = default
    );
}