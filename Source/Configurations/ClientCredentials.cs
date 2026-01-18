namespace Vinder.Federation.Sdk.Configurations;

public sealed record ClientCredentials
{
    public string ClientId { get; init; } = default!;
    public string ClientSecret { get; init; } = default!;
    public string Tenant { get; init; } = default!;
}
