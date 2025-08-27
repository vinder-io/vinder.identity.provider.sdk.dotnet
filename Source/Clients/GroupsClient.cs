namespace Vinder.IdentityProvider.Sdk.Clients;

public sealed class GroupsClient(HttpClient httpClient) : IGroupsClient
{
    public async Task<Result<GroupDetails>> CreateGroupAsync(GroupForCreation group, CancellationToken cancellation = default)
    {
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
