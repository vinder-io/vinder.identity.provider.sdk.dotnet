namespace Vinder.Federation.Sdk.Configurations;

public sealed record FederationOptions
{
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
    public string Tenant { get; set; } = default!;
    public string BaseUrl { get; set; } = default!;
}