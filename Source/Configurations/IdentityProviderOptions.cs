namespace Vinder.IdentityProvider.Sdk.Configurations;

public sealed record IdentityProviderOptions
{
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string Tenant { get; set; } = default!;
    public string BaseUrl { get; set; } = default!;
}