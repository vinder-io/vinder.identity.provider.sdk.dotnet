namespace Vinder.IdentityProvider.Sdk.TestSuite.Clients;

public sealed class IdentityClientTests(IdentityProviderFixture server) :
    IClassFixture<IdentityProviderFixture>
{
    private readonly HttpClient _httpClient = server.HttpClient;

    [Fact(DisplayName = "[e2e] when authenticate with valid credentials should succeed")]
    public async Task WhenAuthenticate_WithValidCredentials_ShouldSucceed()
    {
        /* arrange: create an identity client with the proper tenant header and define admin credentials */
        var client = new IdentityClient(_httpClient.WithTenantHeader("master"));
        var credentials = new AuthenticationCredentials
        {
            Username = "admin",
            Password = "admin"
        };

        /* act: send a POST request to the authenticate endpoint using the identity client */
        var result = await client.AuthenticateAsync(credentials);

        /* assert: ensure the authentication was successful and the result contains data */
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Data);

        /* assert: verify that both access and refresh tokens are returned and not empty */
        Assert.NotEmpty(result.Data.AccessToken);
        Assert.NotEmpty(result.Data.RefreshToken);
    }
}