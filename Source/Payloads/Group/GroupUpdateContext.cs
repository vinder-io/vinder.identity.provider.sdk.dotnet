namespace Vinder.IdentityProvider.Sdk.Payloads.Group;

public sealed record GroupUpdateContext
{
    /// <summary>
    /// Identifier of the group to be updated.
    /// This does not mean the group ID itself will be changed.
    /// </summary>
    [JsonIgnore]
    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
}