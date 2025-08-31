namespace Vinder.IdentityProvider.Sdk.Contracts;

public interface IUsersClient
{
    /// <summary>
    /// Retrieves a paginated list of users based on the provided parameters.
    /// </summary>
    /// <param name="parameters">The parameters for fetching users.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{Pagination{UserDetails}}"/> containing a paginated list of user details or an error.</returns>
    /// <remarks>
    /// This method calls the Identity API to get a list of users.
    /// On success, it returns a paginated list of user details.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of user and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method to retrieve a list of users.
    /// </remarks>
    public Task<Result<Pagination<UserDetails>>> GetUsersAsync(
        UsersFetchParameters parameters,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Retrieves a paginated list of permissions for a specific user.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    /// <param name="parameters">The parameters for listing user assigned permissions.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{IReadOnlyCollection{PermissionDetails}}"/> containing the list of permissions or an error.</returns>
    /// <remarks>
    /// This method calls the Identity API to get the permissions of a user.
    /// On success, it returns a list of permission details.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="UserErrors.UserDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-USR-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of user and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method to retrieve permissions for a specific user.
    /// </remarks>
    public Task<Result<IReadOnlyCollection<PermissionDetails>>> GetUserPermissionsAsync(
        Guid userId,
        ListUserAssignedPermissionsParameters? parameters = null,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Retrieves a list of groups for a specific user.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    /// <param name="parameters">The parameters for listing user assigned groups.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{IReadOnlyCollection{GroupBasicDetails}}"/> containing the list of groups or an error.</returns>
    /// <remarks>
    /// This method calls the Identity API to get the groups of a user.
    /// On success, it returns a list of group details.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="UserErrors.UserDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-USR-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of user and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method to retrieve groups for a specific user.
    /// </remarks>
    public Task<Result<IReadOnlyCollection<GroupBasicDetails>>> GetUserGroupsAsync(
        Guid userId,
        ListUserAssignedGroupsParameters? parameters = null,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Deletes a user from the system.
    /// </summary>
    /// <param name="userId">The identifier of the user to be deleted.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to delete a user.
    /// On success, it returns a successful result.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="UserErrors.UserDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-USR-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of user and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically delete users from the system.
    /// </remarks>
    public Task<Result> DeleteUserAsync(
        Guid userId,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Assigns a user to a group.
    /// </summary>
    /// <param name="data">The details of the user and group to be assigned.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to assign a user to a group.
    /// On success, it returns a successful result.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="UserErrors.UserDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-USR-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="GroupErrors.GroupDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-GRP-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="UserErrors.UserAlreadyInGroup"/> — code: <c>#VINDER-IDP-ERR-USR-409</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of user and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically assign users to groups.
    /// </remarks>
    public Task<Result> AssignUserToGroupAsync(
        AssignUserToGroupContext data,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Assigns a permission to a user.
    /// </summary>
    /// <param name="data">The details of the user and permission to be assigned.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to assign a permission to a user.
    /// On success, it returns a successful result.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="UserErrors.UserDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-USR-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="PermissionErrors.PermissionDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-PRM-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="UserErrors.UserAlreadyHasPermission"/> — code: <c>#VINDER-IDP-ERR-USR-410</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of user and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically assign permissions to users.
    /// </remarks>
    public Task<Result> AssignUserPermissionAsync(
        AssignUserPermissionContext data,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Revokes a permission from a user.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    /// <param name="permissionId">The identifier of the permission to be revoked.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to revoke a permission from a user.
    /// On success, it returns a successful result.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="UserErrors.UserDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-USR-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="PermissionErrors.PermissionDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-PRM-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="UserErrors.PermissionNotAssigned"/> — code: <c>#VINDER-IDP-ERR-USR-411</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of user and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically revoke permissions from users.
    /// </remarks>
    public Task<Result> RevokeUserPermissionAsync(
        Guid userId,
        Guid permissionId,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Removes a user from a group.
    /// </summary>
    /// <param name="userId">The identifier of the user.</param>
    /// <param name="groupId">The identifier of the group.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to remove a user from a group.
    /// On success, it returns a successful result.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="UserErrors.UserDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-USR-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="GroupErrors.GroupDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-GRP-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="UserErrors.UserNotInGroup"/> — code: <c>#VINDER-IDP-ERR-USR-412</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of user and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically remove users from groups.
    /// </remarks>
    public Task<Result> RemoveUserFromGroupAsync(
        Guid userId,
        Guid groupId,
        CancellationToken cancellation = default
    );
}