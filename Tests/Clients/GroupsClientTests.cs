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

    [Fact(DisplayName = "[e2e] - when update group with valid data should succeed")]
    public async Task WhenUpdateGroup_WithValidData_ShouldSucceed()
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

        /* arrange: create the groups client */
        var groupsClient = new GroupsClient(_httpClient);
        var group = new GroupForCreation
        {
            Name = "vinder.defaults.groups.to.update"
        };

        var createResult = await groupsClient.CreateGroupAsync(group);

        Assert.True(createResult.IsSuccess);
        Assert.NotNull(createResult.Data);

        /* arrange: prepare update context with the created group Id and new name */
        var updateContext = new GroupUpdateContext
        {
            Id = createResult.Data.Id,
            Name = "vinder.defaults.groups.updated"
        };

        /* act: call the update group async method */
        var updateResult = await groupsClient.UpdateGroupAsync(updateContext);

        /* assert: verify that the group was updated successfully */
        Assert.True(updateResult.IsSuccess);
        Assert.NotNull(updateResult.Data);

        Assert.Equal(updateContext.Id, updateResult.Data.Id);
        Assert.Equal(updateContext.Name, updateResult.Data.Name);
    }

    [Fact(DisplayName = "[e2e] - when update non-existent group should fail")]
    public async Task WhenUpdateNonExistentGroup_ShouldFail()
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

        /* arrange: create the groups client and prepare update context for a non-existent group */
        var groupsClient = new GroupsClient(_httpClient);
        var updateContext = new GroupUpdateContext
        {
            Id = Guid.NewGuid().ToString(),
            Name = "vinder.defaults.groups.non.existent"
        };

        /* act: call the update group async method */
        var updateResult = await groupsClient.UpdateGroupAsync(updateContext);

        /* assert: verify that the update failed and the correct error was returned */
        Assert.False(updateResult.IsSuccess);

        Assert.NotNull(updateResult.Error);
        Assert.Equal(GroupErrors.GroupDoesNotExist, updateResult.Error);
    }

    [Fact(DisplayName = "[e2e] - when delete group with valid data should succeed")]
    public async Task WhenDeleteGroup_WithValidData_ShouldSucceed()
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
            Name = "vinder.defaults.groups.to.delete"
        };

        /* act: call the create group async method */
        var createResult = await groupsClient.CreateGroupAsync(group);

        /* assert: verify that the group was created successfully */
        Assert.True(createResult.IsSuccess);
        Assert.NotNull(createResult.Data);

        /* act: call the delete group async method */
        var deleteResult = await groupsClient.DeleteGroupAsync(Guid.Parse(createResult.Data.Id));

        /* assert: verify that the group was deleted successfully */
        Assert.True(deleteResult.IsSuccess);
    }

    [Fact(DisplayName = "[e2e] - when delete non-existent group should fail")]
    public async Task WhenDeleteNonExistentGroup_ShouldFail()
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

        /* arrange: create the groups client */
        var groupsClient = new GroupsClient(_httpClient);
        var groupId = Guid.NewGuid();

        /* act: call the delete group async method */
        var deleteResult = await groupsClient.DeleteGroupAsync(groupId);

        /* assert: verify that the delete failed and the correct error was returned */
        Assert.False(deleteResult.IsSuccess);

        Assert.NotNull(deleteResult.Error);
        Assert.Equal(GroupErrors.GroupDoesNotExist, deleteResult.Error);
    }
}