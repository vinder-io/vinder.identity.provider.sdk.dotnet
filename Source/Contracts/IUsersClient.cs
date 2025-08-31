namespace Vinder.IdentityProvider.Sdk.Contracts;

public interface IUsersClient
{
    public Task<Result<Pagination<UserDetails>>> GetUsersAsync(
        UsersFetchParameters parameters,
        CancellationToken cancellation = default
    );

    public Task<Result<IReadOnlyCollection<PermissionDetails>>> GetUserPermissionsAsync(
        Guid userId,
        ListUserAssignedPermissionsParameters? parameters = null,
        CancellationToken cancellation = default
    );

    public Task<Result<IReadOnlyCollection<GroupBasicDetails>>> GetUserGroupsAsync(
        Guid userId,
        ListUserAssignedGroupsParameters? parameters = null,
        CancellationToken cancellation = default
    );

    public Task<Result> DeleteUserAsync(
        Guid userId,
        CancellationToken cancellation = default
    );

    public Task<Result> AssignUserToGroupAsync(
        AssignUserToGroupContext data,
        CancellationToken cancellation = default
    );
}