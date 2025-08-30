namespace Vinder.IdentityProvider.Sdk.Payloads.Permission;

public sealed record PermissionForUpdate
{
    [JsonIgnore]
    public Guid PermissionId { get; init; }
    public string Name { get; init; } = default!;
    public string? Description { get; init; } = default!;
}
