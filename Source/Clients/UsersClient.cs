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
        Guid userId, ListUserAssignedPermissionsParameters? parameters = null, CancellationToken cancellation = default)
    {
        string queryString = QueryParametersParser.ToQueryString(parameters);
        string url = $"api/v1/users?{queryString}";

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
}