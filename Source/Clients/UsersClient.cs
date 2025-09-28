namespace Vinder.IdentityProvider.Sdk.Clients;

public sealed class UsersClient(HttpClient httpClient) : IUsersClient
{
    public async Task<Result<Pagination<UserDetails>>> GetUsersAsync(
        UsersFetchParameters parameters, CancellationToken cancellation = default)
    {
        string queryString = QueryParametersParser.ToQueryString(parameters);
        string url = $"api/v1/users?{queryString}";

        var response = await httpClient.GetAsync(url, cancellation);
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result<Pagination<UserDetails>>.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<Pagination<UserDetails>>.Failure(error)
                : Result<Pagination<UserDetails>>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<Pagination<UserDetails>>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<Pagination<UserDetails>>.Success(result)
            : Result<Pagination<UserDetails>>.Failure(SdkErrors.DeserializationFailure);
    }

    public async Task<Result<IReadOnlyCollection<PermissionDetails>>> GetUserPermissionsAsync(
        string userId, ListUserAssignedPermissionsParameters? parameters = null, CancellationToken cancellation = default)
    {
        string queryString = QueryParametersParser.ToQueryString(parameters);
        string url = $"api/v1/users/{userId}/permissions?{queryString}";

        var response = await httpClient.GetAsync(url, cancellation);
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

    public async Task<Result<IReadOnlyCollection<GroupBasicDetails>>> GetUserGroupsAsync(
        string userId, ListUserAssignedGroupsParameters? parameters = null, CancellationToken cancellation = default)
    {
        string queryString = QueryParametersParser.ToQueryString(parameters);
        string url = $"api/v1/users/{userId}/groups?{queryString}";

        var response = await httpClient.GetAsync(url, cancellation);
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result<IReadOnlyCollection<GroupBasicDetails>>.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<IReadOnlyCollection<GroupBasicDetails>>.Failure(error)
                : Result<IReadOnlyCollection<GroupBasicDetails>>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<GroupBasicDetails>>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<IReadOnlyCollection<GroupBasicDetails>>.Success(result)
            : Result<IReadOnlyCollection<GroupBasicDetails>>.Failure(SdkErrors.DeserializationFailure);
    }

    public async Task<Result> DeleteUserAsync(string userId, CancellationToken cancellation = default)
    {
        var response = await httpClient.DeleteAsync($"api/v1/users/{userId}", cancellation);
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

    public async Task<Result> AssignUserToGroupAsync(
        AssignUserToGroupContext data, CancellationToken cancellation = default)
    {
        var response = await httpClient.PostAsJsonAsync($"api/v1/users/{data.UserId}/groups", data, cancellation);
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

    public async Task<Result> AssignUserPermissionAsync(
        AssignUserPermissionContext data, CancellationToken cancellation = default)
    {
        var response = await httpClient.PostAsJsonAsync($"api/v1/users/{data.UserId}/permissions", data, cancellation);
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

    public async Task<Result> RevokeUserPermissionAsync(string userId, string permissionId, CancellationToken cancellation = default)
    {
        var response = await httpClient.DeleteAsync($"api/v1/users/{userId}/permissions/{permissionId}", cancellation);
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

    public async Task<Result> RemoveUserFromGroupAsync(string userId, string groupId, CancellationToken cancellation = default)
    {
        var response = await httpClient.DeleteAsync($"api/v1/users/{userId}/groups/{groupId}", cancellation);
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
}