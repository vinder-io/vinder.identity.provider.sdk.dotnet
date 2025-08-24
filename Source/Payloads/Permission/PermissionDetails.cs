namespace Vinder.IdentityProvider.Sdk.Payloads.Permission;

public sealed record PermissionDetails
{
    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
    public string? Description { get; init; } = default!;
}