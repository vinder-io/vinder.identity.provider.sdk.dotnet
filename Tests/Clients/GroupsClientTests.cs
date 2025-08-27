namespace Vinder.IdentityProvider.Sdk.TestSuite.Clients;

public sealed class GroupsClientTests(IdentityProviderFixture server) :
    IClassFixture<IdentityProviderFixture>
{
    private readonly HttpClient _httpClient = server.HttpClient;

    [Fact(DisplayName = "[e2e] - when create group with valid data should succeed")]
    public async Task WhenCreateGroup_WithValidData_ShouldSucceed()
    {

    }
}