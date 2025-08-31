namespace Vinder.IdentityProvider.Sdk.Contracts;

public interface IGroupsClient
{
    /// <summary>
    /// Creates a new group in the system.
    /// </summary>
    /// <param name="group">The details of the group to be created.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{GroupDetails}"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to create a new group.  
    /// On success, it returns the details of the created group.  
    /// On failure, the returned result contains detailed error information, including an error code and description.  
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="GroupErrors.GroupAlreadyExists"/> — code: <c>#VINDER-IDP-ERR-GRP-409</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of group and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.  
    /// Use this method when you need to programmatically create groups in the system.
    /// </remarks>
    public Task<Result<GroupDetails>> CreateGroupAsync(
        GroupForCreation group,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Updates an existing group in the system.
    /// </summary>
    /// <param name="group">The update context containing the group identifier and new values.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{GroupDetails}"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to update a group.
    /// On success, it returns the updated group details.
    /// On failure, the returned result contains detailed error information, including an error code and description.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="GroupErrors.GroupDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-GRP-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of group and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically update groups in the system.
    /// </remarks>
    public Task<Result<GroupDetails>> UpdateGroupAsync(
        GroupUpdateContext group,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Deletes a group from the system.
    /// </summary>
    /// <param name="groupId">The identifier of the group to be deleted.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to delete a group.
    /// On success, it returns a successful result.
    /// On failure, the returned result contains detailed error information, including an error code and description.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="GroupErrors.GroupDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-GRP-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of group and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically delete groups from the system.
    /// </remarks>
    public Task<Result> DeleteGroupAsync(
        Guid groupId,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Assigns a permission to a group.
    /// </summary>
    /// <param name="assignGroupPermission">The details of the permission to be assigned to the group.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to assign a permission to a group.  
    /// On success, it returns a successful result.  
    /// On failure, the returned result contains detailed error information, including an error code and description.  
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="GroupErrors.GroupDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-GRP-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="PermissionErrors.PermissionDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-PRM-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="GroupErrors.GroupAlreadyHasPermission"/> — code: <c>#VINDER-IDP-ERR-GRP-415</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of group and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.  
    /// Use this method when you need to programmatically assign permissions to groups in the system.
    /// </remarks>
    public Task<Result<GroupDetails>> AssignGroupPermissionAsync(
        AssignGroupPermission data,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Revokes a permission from a group.
    /// </summary>
    /// <param name="groupId">The identifier of the group.</param>
    /// <param name="permissionId">The identifier of the permission to be revoked.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to revoke a permission from a group.
    /// On success, it returns a successful result.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="GroupErrors.GroupDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-GRP-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="PermissionErrors.PermissionDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-PRM-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="GroupErrors.PermissionNotAssigned"/> — code: <c>#VINDER-IDP-ERR-GRP-409</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of group and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically revoke permissions from groups.
    /// </remarks>
    public Task<Result> RevokeGroupPermissionAsync(
        Guid groupId,
        Guid permissionId,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Retrieves a paginated list of permissions assigned to a group.
    /// </summary>
    /// <param name="groupId">The identifier of the group.</param>
    /// <param name="parameters">The query parameters for pagination and filtering.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{T}"/> containing the list of permissions or an error.</returns>
    /// <remarks>
    /// This method calls the Identity API to get the permissions of a group.
    /// On success, it returns a paginated list of permission details.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="GroupErrors.GroupDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-GRP-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of group and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method to retrieve permissions assigned to a specific group.
    /// </remarks>
    public Task<Result<IReadOnlyCollection<PermissionDetails>>> GetGroupPermissionsAsync(
        Guid groupId,
        ListGroupPermissionsParameters? parameters = null,
        CancellationToken cancellation = default
    );
}
