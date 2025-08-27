namespace Vinder.IdentityProvider.Sdk.TestSuite.Clients;

public sealed class GroupsClientTests(IdentityProviderFixture server) :
    IClassFixture<IdentityProviderFixture>
{
    private readonly HttpClient _httpClient = server.HttpClient;

    [Fact(DisplayName = "[e2e] - when create group with valid data should succeed")]
    public async Task WhenCreateGroup_WithValidData_ShouldSucceed()
    {
        /* arrange: create an identity client with the proper tenant header and define admin credentials */
        var identityClient = new IdentityClient(_httpClient.WithTenantHeader("master"));
        var credentials = new AuthenticationCredentials
        {
            Username = "admin",
            Password = "admin"
        };

        /* act: send a POST request to the authenticate endpoint using the identity client */
        var authenticationResult = await identityClient.AuthenticateAsync(credentials);

        /* assert: ensure the authentication was successful and the result contains data */
        Assert.True(authenticationResult.IsSuccess);
        Assert.NotNull(authenticationResult.Data);

        _httpClient.WithAuthorization(authenticationResult.Data.AccessToken);

        /* arrange: create the groups client and the group to create */
        var groupsClient = new GroupsClient(_httpClient);
        var group = new GroupForCreation
        {
            Name = "vinder.defaults.groups.testing"
        };

        /* act: call the create group async method */
        var result = await groupsClient.CreateGroupAsync(group);

        /* assert: verify that the group was created successfully */
        Assert.True(result.IsSuccess);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Id);

        Assert.Empty(result.Data.Permissions);
        Assert.Equal(group.Name, result.Data.Name);
    }
}