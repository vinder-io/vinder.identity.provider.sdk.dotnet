namespace Vinder.Federation.Sdk.TestSuite.Clients;

public sealed class TenantsClientTests(IdentityProviderFixture server) :
    IClassFixture<IdentityProviderFixture>
{
    private readonly HttpClient _httpClient = server.HttpClient;

    [Fact(DisplayName = "[e2e] - when create tenant with valid data should succeed")]
    public async Task WhenCreateTenant_WithValidData_ShouldSucceed()
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

        /* arrange: create the tenants client and the tenant to create */
        var tenantsClient = new TenantsClient(_httpClient);
        var tenant = new TenantCreationScheme
        {
            Name = "vinder.defaults.tenants.testing",
            Description = "Tenant for testing purposes"
        };

        /* act: call the create tenant async method */
        var result = await tenantsClient.CreateTenantAsync(tenant);

        /* assert: verify that the tenant was created successfully */
        Assert.True(result.IsSuccess);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.Id);

        Assert.Equal(tenant.Name, result.Data.Name);
        Assert.Equal(tenant.Description, result.Data.Description);
    }

    [Fact(DisplayName = "[e2e] - when create tenant with existing name should fail")]
    public async Task WhenCreateTenant_WithExistingName_ShouldFail()
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

        /* arrange: create the tenants client and the tenant to create */
        var tenantsClient = new TenantsClient(_httpClient);
        var tenant = new TenantCreationScheme
        {
            Name = "vinder.defaults.tenants.existing",
            Description = "Tenant for testing purposes"
        };

        /* act: call the create tenant async method twice */
        await tenantsClient.CreateTenantAsync(tenant);

        var result = await tenantsClient.CreateTenantAsync(tenant);

        /* assert: verify that the second creation failed with TenantAlreadyExists error */
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Error);

        Assert.Equal(TenantErrors.TenantAlreadyExists, result.Error);
    }

    [Fact(DisplayName = "[e2e] - when update tenant with valid data should succeed")]
    public async Task WhenUpdateTenant_WithValidData_ShouldSucceed()
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

        /* arrange: create the tenants client and the tenant to create */
        var tenantsClient = new TenantsClient(_httpClient);
        var tenantToCreate = new TenantCreationScheme
        {
            Name = "vinder.defaults.tenants.to.update",
            Description = "Tenant to be updated"
        };

        var createResult = await tenantsClient.CreateTenantAsync(tenantToCreate);

        Assert.True(createResult.IsSuccess);
        Assert.NotNull(createResult.Data);

        /* arrange: prepare update context with the created tenant Id and new name/description */
        var payload = new TenantUpdateScheme
        {
            TenantId = createResult.Data.Id,
            Name = "vinder.defaults.tenants.updated",
            Description = "Updated tenant description"
        };

        /* act: call the update tenant async method */
        var updateResult = await tenantsClient.UpdateTenantAsync(payload);

        /* assert: verify that the tenant was updated successfully */
        Assert.True(updateResult.IsSuccess);
        Assert.NotNull(updateResult.Data);

        Assert.Equal(createResult.Data.Id, updateResult.Data.Id);
        Assert.Equal(payload.Name, updateResult.Data.Name);
        Assert.Equal(payload.Description, updateResult.Data.Description);
    }

    [Fact(DisplayName = "[e2e] - when update non-existent tenant should fail")]
    public async Task WhenUpdateNonExistentTenant_ShouldFail()
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

        /* arrange: create the tenants client and prepare update context for a non-existent tenant */
        var tenantsClient = new TenantsClient(_httpClient);
        var tenantToUpdate = new TenantUpdateScheme
        {
            TenantId = "tenant_Jdahsdn18781263",
            Name = "vinder.defaults.tenants.non.existent",
            Description = "non-existent tenant"
        };

        /* act: call the update tenant async method */
        var updateResult = await tenantsClient.UpdateTenantAsync(tenantToUpdate);

        /* assert: verify that the update failed and the correct error was returned */
        Assert.True(updateResult.IsFailure);

        Assert.NotNull(updateResult.Error);
        Assert.Equal(TenantErrors.TenantDoesNotExist, updateResult.Error);
    }

    [Fact(DisplayName = "[e2e] - when delete tenant with valid data should succeed")]
    public async Task WhenDeleteTenant_WithValidData_ShouldSucceed()
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

        /* arrange: create the tenants client and the tenant to create */
        var tenantsClient = new TenantsClient(_httpClient);
        var tenantToCreate = new TenantCreationScheme
        {
            Name = "vinder.defaults.tenants.to.delete",
            Description = "Tenant to be deleted"
        };

        /* act: call the create tenant async method */
        var createResult = await tenantsClient.CreateTenantAsync(tenantToCreate);

        /* assert: verify that the tenant was created successfully */
        Assert.True(createResult.IsSuccess);
        Assert.NotNull(createResult.Data);

        /* act: call the delete tenant async method */
        var deleteResult = await tenantsClient.DeleteTenantAsync(createResult.Data.Id);

        /* assert: verify that the tenant was deleted successfully */
        Assert.True(deleteResult.IsSuccess);
    }

    [Fact(DisplayName = "[e2e] - when delete non-existent tenant should fail")]
    public async Task WhenDeleteNonExistentTenant_ShouldFail()
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

        /* arrange: create the tenants client */
        var tenantsClient = new TenantsClient(_httpClient);
        var nonExistentTenantId = "tenant_Jdahsdn18781263";

        /* act: call the delete tenant async method */
        var deleteResult = await tenantsClient.DeleteTenantAsync(nonExistentTenantId);

        /* assert: verify that the delete failed and the correct error was returned */
        Assert.False(deleteResult.IsSuccess);

        Assert.NotNull(deleteResult.Error);
        Assert.Equal(TenantErrors.TenantDoesNotExist, deleteResult.Error);
    }
}