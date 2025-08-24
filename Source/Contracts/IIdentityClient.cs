namespace Vinder.IdentityProvider.Sdk.Contracts;

public interface IIdentityClient
{
    /// <summary>
    /// Authenticates a user using the provided credentials.
    /// </summary>
    /// <param name="credentials">The credentials to authenticate the user.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result{AuthenticationResult}"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to authenticate a user.
    /// On success, it returns the authentication result containing tokens.
    /// On failure, the returned result contains detailed error information, including an error code and description.
    /// You can check all possible errors and their meanings here: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// Use this when you need to log in a user and obtain their access tokens.
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="AuthenticationErrors.InvalidCredentials"/> — code: <c>#VINDER-IDP-ERR-AUT-401</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="AuthenticationErrors.UserNotFound"/> — code: <c>#VINDER-IDP-ERR-AUT-404</c></description>
    ///   </item>
    /// </list>
    /// </remarks>
    public Task<Result<AuthenticationResult>> AuthenticateAsync(
        AuthenticationCredentials credentials,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Creates a new identity (user) in the system.
    /// </summary>
    /// <param name="credentials">The enrollment credentials for the new identity.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method calls the Identity API to register a new user programmatically.  
    /// On failure, the returned result contains detailed error information, including an error code and description.  
    ///
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="IdentityErrors.UserAlreadyExists"/> — code: <c>#VINDER-IDP-ERR-IDN-409</c></description>
    ///   </item>
    /// </list>
    ///
    /// For a full list of identity errors and their meanings, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.  
    /// Use this when you need to onboard new users without manual intervention.
    /// </remarks>
    public Task<Result> CreateIdentityAsync(
        IdentityEnrollmentCredentials credentials,
        CancellationToken cancellation = default
    );

    /// <summary>
    /// Invalidates a user session by revoking the refresh token.
    /// </summary>
    /// <param name="session">The session object containing the refresh token to revoke.</param>
    /// <param name="cancellation">A cancellation token to cancel the operation.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    /// <remarks>
    /// This method is used to revoke a refresh token, effectively invalidating the user's session.  
    /// On failure, the returned result contains detailed error information, including an error code and description.  
    /// <para>The following errors may occur when calling this method:</para>
    /// <list type="bullet">
    ///   <item>
    ///     <description><see cref="AuthenticationErrors.InvalidRefreshToken"/> — code: <c>#VINDER-IDP-ERR-AUT-405</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="AuthenticationErrors.TokenExpired"/> — code: <c>#VINDER-IDP-ERR-AUT-411</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="AuthenticationErrors.InvalidSignature"/> — code: <c>#VINDER-IDP-ERR-AUT-412</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="AuthenticationErrors.LogoutFailed"/> — code: <c>#VINDER-IDP-ERR-AUT-409</c></description>
    ///   </item>
    ///   <item>
    ///     <description><see cref="AuthenticationErrors.InvalidTokenFormat"/> — code: <c>#VINDER-IDP-ERR-AUT-410</c></description>
    ///   </item>
    /// </list>
    /// For a full list of authentication errors and their meanings, see: <see href="https://bit.ly/errors-reference">Errors Reference</see>.
    /// </remarks>
    public Task<Result> InvalidateSessionAsync(
        SessionInvalidation session,
        CancellationToken cancellation = default
    );
}