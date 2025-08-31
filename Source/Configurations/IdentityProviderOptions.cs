namespace Vinder.IdentityProvider.Sdk.Configurations;

public sealed record IdentityProviderOptions
{
    public string ClientId { get; init; } = default!;
    public string ClientSecret { get; init; } = default!;
    public string Tenant { get; init; } = default!;
    public string BaseUrl { get; init; } = default!;
}