namespace Vinder.IdentityProvider.Sdk.Contracts;

/// <summary>
/// Defines the contract for a client that interacts with the permissions endpoint of the Identity Provider.
/// </summary>
public interface IPermissionsClient
{
    /// <summary>
    /// Retrieves a paginated list of permissions based on the specified criteria.
    /// </summary>
    /// <param name="parameters">The parameters to filter and paginate the permissions.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{Pagination}"/> containing a paginated list of <see cref="PermissionDetails"/>.</returns>
    /// <remarks>
    /// This method calls the Identity API to retrieve a list of permissions.  
    /// On success, it returns a paginated list of permission details.  
    /// On failure, the returned result contains detailed error information, including an error code and description.  
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of permission and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.  
    /// Use this method when you need to programmatically fetch permissions from the system.
    /// </remarks>
    public Task<Result<Pagination<PermissionDetails>>> GetPermissionsAsync(
        PermissionsFetchParameters parameters,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Creates a new permission in the system.
    /// </summary>
    /// <param name="permission">The details of the permission to be created.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{PermissionDetails}"/> containing the details of the created permission.</returns>
    /// <remarks>
    /// This method calls the Identity API to create a new permission.  
    /// On success, it returns the details of the created permission.  
    /// On failure, the returned result contains detailed error information, including an error code and description.  
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="PermissionErrors.PermissionAlreadyExists"/> — code: <c>#VINDER-IDP-ERR-PRM-409</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of permission and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.  
    /// Use this method when you need to programmatically create permissions in the system.
    /// </remarks>
    public Task<Result<PermissionDetails>> CreatePermissionAsync(
        PermissionForCreation permission,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Updates an existing permission in the system.
    /// </summary>
    /// <param name="permission">The update context containing the permission identifier and new values.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{PermissionDetails}"/> containing the updated permission details.</returns>
    /// <remarks>
    /// This method calls the Identity API to update a permission.  
    /// On success, it returns the updated permission details.  
    /// On failure, the returned result contains detailed error information, including an error code and description.  
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="PermissionErrors.PermissionDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-PRM-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of permission and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.  
    /// Use this method when you need to programmatically update permissions in the system.
    /// </remarks>
    public Task<Result<PermissionDetails>> UpdatePermissionAsync(
        PermissionForUpdate permission,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Deletes a permission from the system.
    /// </summary>
    /// <param name="permissionId">The identifier of the permission to be deleted.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to delete a permission.  
    /// On success, it returns a successful result.  
    /// On failure, the returned result contains detailed error information, including an error code and description.  
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="PermissionErrors.PermissionDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-PRM-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of permission and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.  
    /// Use this method when you need to programmatically delete permissions from the system.
    /// </remarks>
    public Task<Result> DeletePermissionAsync(Guid permissionId, CancellationToken cancellation = default);
}