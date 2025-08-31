namespace Vinder.IdentityProvider.Sdk.Payloads.Group;

public sealed record ListUserAssignedGroupsParameters
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 60;
}
