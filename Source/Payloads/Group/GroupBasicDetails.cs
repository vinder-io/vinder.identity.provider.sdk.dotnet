namespace Vinder.IdentityProvider.Sdk.Payloads.Group;

public sealed record GroupBasicDetails
{
    public string Id { get; init; } = default!;
    public string Name { get; init; } = default!;
}
