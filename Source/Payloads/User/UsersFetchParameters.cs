namespace Vinder.IdentityProvider.Sdk.Payloads.User;

public sealed record UsersFetchParameters
{
    public Guid? Id { get; init; }
    public string? Username { get; init; }
    public bool? IsDeleted { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 60;
}