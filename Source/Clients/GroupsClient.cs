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

    public async Task<Result<GroupDetails>> UpdateGroupAsync(GroupUpdateContext group, CancellationToken cancellation = default)
    {
        var response = await httpClient.PutAsJsonAsync($"api/v1/groups/{group.Id}", group, cancellation);
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

    public async Task<Result> DeleteGroupAsync(Guid groupId, CancellationToken cancellation = default)
    {
        var response = await httpClient.DeleteAsync($"api/v1/groups/{groupId}", cancellation);
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result.Failure(error)
                : Result.Failure(SdkErrors.DeserializationFailure);
        }

        return Result.Success();
    }

    public async Task<Result<GroupDetails>> AssignGroupPermissionAsync(AssignGroupPermission data, CancellationToken cancellation = default)
    {
        var response = await httpClient.PostAsJsonAsync($"api/v1/groups/{data.GroupId}/permissions", data, cancellation);
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

    public async Task<Result> RevokeGroupPermissionAsync(Guid groupId, Guid permissionId, CancellationToken cancellation = default)
    {
        var response = await httpClient.DeleteAsync($"api/v1/groups/{groupId}/permissions/{permissionId}", cancellation);
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result.Failure(error)
                : Result.Failure(SdkErrors.DeserializationFailure);
        }

        return Result.Success();
    }

    public async Task<Result<IReadOnlyCollection<PermissionDetails>>> GetGroupPermissionsAsync(Guid groupId, ListGroupPermissionsParameters parameters, CancellationToken cancellation = default)
    {
        var queryString = QueryParametersParser.ToQueryString(parameters);
        var response = await httpClient.GetAsync($"api/v1/groups/{groupId}/permissions?{queryString}", cancellation);

        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result<IReadOnlyCollection<PermissionDetails>>.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<IReadOnlyCollection<PermissionDetails>>.Failure(error)
                : Result<IReadOnlyCollection<PermissionDetails>>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<PermissionDetails>>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<IReadOnlyCollection<PermissionDetails>>.Success(result)
            : Result<IReadOnlyCollection<PermissionDetails>>.Failure(SdkErrors.DeserializationFailure);
    }
}
