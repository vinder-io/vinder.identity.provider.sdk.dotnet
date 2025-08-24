namespace Vinder.IdentityProvider.Sdk.Contracts;

public interface IIdentityClient
{
    public Task<Result<AuthenticationResult>> AuthenticateAsync(
        AuthenticationCredentials credentials,
        CancellationToken cancellation = default
    );

    public Task<Result> CreateIdentityAsync(
        IdentityEnrollmentCredentials credentials,
        CancellationToken cancellation = default
    );
}