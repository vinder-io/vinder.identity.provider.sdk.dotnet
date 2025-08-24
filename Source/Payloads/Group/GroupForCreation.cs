namespace Vinder.IdentityProvider.Sdk.Payloads.Group;

public sealed record GroupForCreation
{
    public string Name { get; init; } = default!;
}