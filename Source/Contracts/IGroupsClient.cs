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
}