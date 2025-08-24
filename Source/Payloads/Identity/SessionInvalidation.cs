namespace Vinder.IdentityProvider.Sdk.Payloads.Identity;

public sealed record SessionInvalidation
{
    public string RefreshToken { get; init; } = default!;
}