namespace Vinder.IdentityProvider.Sdk.Payloads.Tenant;

public sealed record TenantForCreation
{
    public string Name { get; init; } = default!;
    public string? Description { get; init; } = default!;
}
