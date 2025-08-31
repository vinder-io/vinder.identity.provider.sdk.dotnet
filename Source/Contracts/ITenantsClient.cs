namespace Vinder.IdentityProvider.Sdk.Contracts;

public interface ITenantsClient
{
    /// <summary>
    /// Retrieves a paginated list of tenants based on the provided parameters.
    /// </summary>
    /// <param name="parameters">The parameters for fetching tenants.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{Pagination{TenantDetails}}"/> containing a paginated list of tenant details or an error.</returns>
    /// <remarks>
    /// This method calls the Identity API to get a list of tenants.
    /// On success, it returns a paginated list of tenant details.
    /// On failure, the returned result contains detailed error information.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of tenant and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method to retrieve a list of tenants.
    /// </remarks>
    public Task<Result<Pagination<TenantDetails>>> GetTenantsAsync(
        TenantFetchParameters parameters,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Creates a new tenant in the system.
    /// </summary>
    /// <param name="tenant">The details of the tenant to be created.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{TenantDetails}"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to create a new tenant.
    /// On success, it returns the details of the created tenant.
    /// On failure, the returned result contains detailed error information, including an error code and description.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="TenantErrors.TenantAlreadyExists"/> — code: <c>#VINDER-IDP-ERR-TNT-409</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of tenant and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically create tenants in the system.
    /// </remarks>
    public Task<Result<TenantDetails>> CreateTenantAsync(
        TenantForCreation tenant,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Updates an existing tenant in the system.
    /// </summary>
    /// <param name="tenant">The update context containing the tenant identifier and new values.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{TenantDetails}"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to update a tenant.
    /// On success, it returns the updated tenant details.
    /// On failure, the returned result contains detailed error information, including an error code and description.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="TenantErrors.TenantDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-TNT-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of tenant and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically update tenants in the system.
    /// </remarks>
    public Task<Result<TenantDetails>> UpdateTenantAsync(
        TenantForUpdate tenant,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Deletes a tenant from the system.
    /// </summary>
    /// <param name="tenantId">The identifier of the tenant to be deleted.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to delete a tenant.
    /// On success, it returns a successful result.
    /// On failure, the returned result contains detailed error information, including an error code and description.
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="TenantErrors.TenantDoesNotExist"/> — code: <c>#VINDER-IDP-ERR-TNT-404</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="SdkErrors.Unauthorized"/> — code: <c>#VINDER-SDK-ERR-003</c> (if the caller lacks permission)</description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of tenant and authentication errors, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this method when you need to programmatically delete tenants from the system.
    /// </remarks>
    public Task<Result> DeleteTenantAsync(
        Guid tenantId,
        CancellationToken cancellation = default
    );
}
