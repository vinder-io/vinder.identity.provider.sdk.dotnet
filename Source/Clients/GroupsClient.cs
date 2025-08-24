namespace Vinder.IdentityProvider.Sdk.Clients;

public sealed class GroupsClient(HttpClient httpClient, IOpenIDConnectClient openIDClient, ClientCredentials clientCredentials) : IGroupsClient
{
    public async Task<Result<GroupDetails>> CreateGroupAsync(GroupForCreation group, CancellationToken cancellation = default)
    {
        var credentials = new ClientAuthenticationCredentials
        {
            ClientId = clientCredentials.ClientId,
            ClientSecret = clientCredentials.ClientSecret
        };

        var authenticationResult = await openIDClient.AuthenticateAsync(credentials, cancellation);
        if (authenticationResult.IsFailure || authenticationResult.Data is null)
        {
            return Result<GroupDetails>.Failure(authenticationResult.Error);
        }

        httpClient.WithAuthorization(authenticationResult.Data.AccessToken)
            .WithTenantHeader(clientCredentials.Tenant);

        var response = await httpClient.PostAsJsonAsync("api/v1/groups", group, cancellation);
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result<GroupDetails>.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<GroupDetails>.Failure(error)
                : Result<GroupDetails>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<GroupDetails>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<GroupDetails>.Success(result)
            : Result<GroupDetails>.Failure(SdkErrors.DeserializationFailure);
    }
}
