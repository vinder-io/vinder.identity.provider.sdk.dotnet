namespace Vinder.IdentityProvider.Sdk.TestSuite.Clients;

public sealed class IdentityClientTests(IdentityProviderFixture server) :
    IClassFixture<IdentityProviderFixture>
{
    private readonly HttpClient _httpClient = server.HttpClient;

    [Fact(DisplayName = "[e2e] - when authenticate with valid credentials should succeed")]
    public async Task WhenAuthenticate_WithValidCredentials_ShouldSucceed()
    {
        /* arrange: create an identity client with the proper tenant header and define admin credentials */
        var identityClient = new IdentityClient(_httpClient.WithTenantHeader("master"));
        var credentials = new AuthenticationCredentials
        {
            Username = "admin",
            Password = "admin"
        };

        /* act: send a POST request to the authenticate endpoint using the identity client */
        var result = await identityClient.AuthenticateAsync(credentials);

        /* assert: ensure the authentication was successful and the result contains data */
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);

        /* assert: verify that both access and refresh tokens are returned and not empty */
        Assert.NotEmpty(result.Data.AccessToken);
        Assert.NotEmpty(result.Data.RefreshToken);
    }

    [Fact(DisplayName = "[e2e] - when authenticate with non-existent user should return #VINDER-IDP-ERR-AUT-404 error")]
    public async Task WhenAuthenticate_WithNonExistentUser_ShouldReturnUserNotFound()
    {
        /* arrange: create an identity client with the proper tenant header */
        var identityClient = new IdentityClient(_httpClient.WithTenantHeader("master"));

        /* arrange: define credentials for a user that does not exist */
        var credentials = new AuthenticationCredentials
        {
            Username = "non.existent.user",
            Password = "somepassword"
        };

        /* act: attempt to authenticate the non-existent user */
        var result = await identityClient.AuthenticateAsync(credentials);

        /* assert: ensure the authentication failed */
        Assert.False(result.IsSuccess);
        Assert.Equal(AuthenticationErrors.UserNotFound, result.Error);
    }

    [Fact(DisplayName = "[e2e] - when authenticate with valid user but wrong password should return #VINDER-IDP-ERR-AUT-401 error")]
    public async Task WhenAuthenticate_WithValidUserButWrongPassword_ShouldReturnInvalidCredentials()
    {
        /* arrange: create an identity client with the proper tenant header */
        var identityClient = new IdentityClient(_httpClient.WithTenantHeader("master"));

        /* arrange: define credentials with an existing username but wrong password */
        var credentials = new AuthenticationCredentials
        {
            Username = "admin",
            Password = "wrongpassword"
        };

        /* act: attempt to authenticate with invalid credentials */
        var result = await identityClient.AuthenticateAsync(credentials);

        /* assert: ensure the authentication failed */
        Assert.True(result.IsFailure);
        Assert.Equal(AuthenticationErrors.InvalidCredentials, result.Error);
    }

    [Fact(DisplayName = "[e2e] - when authenticate without tenant header should return #VINDER-IDP-ERR-TNT-400 error")]
    public async Task WhenAuthenticate_WithoutTenantHeader_ShouldReturnTenantHeaderMissing()
    {
        /* arrange: ensure tenant header is removed before creating the identity client */
        const string tenantHeader = "x-tenant";

        if (_httpClient.DefaultRequestHeaders.Contains(tenantHeader))
            _httpClient.DefaultRequestHeaders.Remove(tenantHeader);

        /* arrange: create an identity client without setting the tenant header */
        var identityClient = new IdentityClient(_httpClient);

        /* arrange: define valid credentials */
        var credentials = new AuthenticationCredentials
        {
            Username = "admin",
            Password = "admin"
        };

        /* act: attempt to authenticate without tenant header */
        var result = await identityClient.AuthenticateAsync(credentials);

        /* assert: ensure the authentication failed */
        Assert.True(result.IsFailure);
        Assert.Equal(TenantErrors.TenantHeaderMissing, result.Error);
    }

    [Fact(DisplayName = "[e2e] - when authenticate with non-existent tenant should return #VINDER-IDP-ERR-TNT-404 error")]
    public async Task WhenAuthenticate_WithNonExistentTenant_ShouldReturnTenantDoesNotExist()
    {
        /* arrange: create an identity client with a non-existent tenant header */
        var identityClient = new IdentityClient(_httpClient.WithTenantHeader("non-existent-tenant"));

        /* arrange: define valid credentials */
        var credentials = new AuthenticationCredentials
        {
            Username = "admin",
            Password = "admin"
        };

        /* act: attempt to authenticate with non-existent tenant */
        var result = await identityClient.AuthenticateAsync(credentials);

        /* assert: ensure the authentication failed */
        Assert.True(result.IsFailure);
        Assert.Equal(TenantErrors.TenantDoesNotExist, result.Error);
    }

    [Fact(DisplayName = "[e2e] - when create identity with valid credentials should succeed and authenticate successfully")]
    public async Task WhenCreateIdentity_WithValidCredentials_ShouldSucceedAndAuthenticate()
    {
        /* arrange: create an identity client with the proper tenant header */
        var identityClient = new IdentityClient(_httpClient.WithTenantHeader("master"));

        /* arrange: define enrollment credentials for a new user */
        var credentials = new IdentityEnrollmentCredentials
        {
            Username = $"user_{Guid.NewGuid():N}",
            Password = "SuperSecret123!"
        };

        /* act: create the identity */
        var creationResult = await identityClient.CreateIdentityAsync(credentials);

        /* assert: ensure the identity creation was successful */
        Assert.True(creationResult.IsSuccess);

        /* arrange: prepare authentication credentials for the created user */
        var authenticationCredentials = new AuthenticationCredentials
        {
            Username = credentials.Username,
            Password = credentials.Password
        };

        /* act: attempt to authenticate with the newly created identity */
        var authenticationResult = await identityClient.AuthenticateAsync(authenticationCredentials);

        /* assert: ensure the authentication was successful */
        Assert.True(authenticationResult.IsSuccess);
        Assert.NotNull(authenticationResult.Data);

        /* assert: verify that both access and refresh tokens are returned and not empty */
        Assert.NotEmpty(authenticationResult.Data.AccessToken);
        Assert.NotEmpty(authenticationResult.Data.RefreshToken);
    }

    [Fact(DisplayName = "[e2e] - when create identity with an existing username should fail with conflict error")]
    public async Task WhenCreateIdentity_WithExistingUsername_ShouldFailWithConflict()
    {
        /* arrange: create an identity client with the proper tenant header */
        var identityClient = new IdentityClient(_httpClient.WithTenantHeader("master"));

        /* arrange: define fixed enrollment credentials for the user */
        var credentials = new IdentityEnrollmentCredentials
        {
            Username = $"user_{Guid.NewGuid():N}",
            Password = "SuperSecret123!"
        };

        /* act: first creation should succeed */
        var firstCreationResult = await identityClient.CreateIdentityAsync(credentials);

        /* act: second creation with the same username */
        var secondCreationResult = await identityClient.CreateIdentityAsync(credentials);

        /* assert: ensure the second creation failed */
        Assert.True(firstCreationResult.IsSuccess);
        Assert.False(secondCreationResult.IsSuccess);

        Assert.NotNull(secondCreationResult.Error);

        /* assert: verify the correct conflict error was returned */
        Assert.Equal(IdentityErrors.UserAlreadyExists.Code, secondCreationResult.Error.Code);
        Assert.Equal(IdentityErrors.UserAlreadyExists.Description, secondCreationResult.Error.Description);
    }
}