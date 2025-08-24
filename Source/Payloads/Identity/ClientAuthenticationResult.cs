namespace Vinder.IdentityProvider.Sdk.Payloads.Identity;

public sealed record ClientAuthenticationResult
{
    public string AccessToken { get; init; } = default!;
}