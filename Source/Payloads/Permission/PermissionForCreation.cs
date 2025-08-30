namespace Vinder.IdentityProvider.Sdk.Payloads.Permission;

public sealed record PermissionForCreation
{
    public string Name { get; init; } = default!;
    public string? Description { get; init; } = default!;
}
