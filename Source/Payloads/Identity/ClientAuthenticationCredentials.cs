namespace Vinder.IdentityProvider.Sdk.Payloads.Identity;

public sealed record ClientAuthenticationCredentials
{
    public string GrantType { get; init; } = default!;
    public string ClientId { get; init; } = default!;
    public string ClientSecret { get; init; } = default!;
}