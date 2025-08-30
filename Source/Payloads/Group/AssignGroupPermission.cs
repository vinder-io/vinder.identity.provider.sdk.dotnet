
using System.Text.Json.Serialization;

namespace Vinder.IdentityProvider.Sdk.Payloads.Group;

public sealed record AssignGroupPermission
{
    [JsonIgnore]
    public Guid GroupId { get; init; }

    public string PermissionName { get; init; } = default!;
}
