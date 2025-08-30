namespace Vinder.IdentityProvider.Sdk.Clients;

public sealed class PermissionsClient(HttpClient httpClient) : IPermissionsClient
{
    public async Task<Result<PermissionDetails>> CreatePermissionAsync(PermissionForCreation permission, CancellationToken cancellation = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/v1/permissions", permission, cancellation);
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result<PermissionDetails>.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<PermissionDetails>.Failure(error)
                : Result<PermissionDetails>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<PermissionDetails>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<PermissionDetails>.Success(result)
            : Result<PermissionDetails>.Failure(SdkErrors.DeserializationFailure);
    }

    public async Task<Result<PermissionDetails>> UpdatePermissionAsync(PermissionForUpdate permission, CancellationToken cancellation = default)
    {
        var response = await httpClient.PutAsJsonAsync($"api/v1/permissions/{permission.PermissionId}", permission, cancellation);
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result<PermissionDetails>.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<PermissionDetails>.Failure(error)
                : Result<PermissionDetails>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<PermissionDetails>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<PermissionDetails>.Success(result)
            : Result<PermissionDetails>.Failure(SdkErrors.DeserializationFailure);
    }

    public async Task<Result> DeletePermissionAsync(Guid permissionId, CancellationToken cancellation = default)
    {
        var response = await httpClient.DeleteAsync($"api/v1/permissions/{permissionId}", cancellation);
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

    public async Task<Result<Pagination<PermissionDetails>>> GetPermissionsAsync(PermissionsFetchParameters parameters, CancellationToken cancellation = default)
    {
        var queryString = QueryParametersParser.ToQueryString(parameters);

        var response = await httpClient.GetAsync($"api/v1/permissions?{queryString}", cancellation);
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result<Pagination<PermissionDetails>>.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<Pagination<PermissionDetails>>.Failure(error)
                : Result<Pagination<PermissionDetails>>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<Pagination<PermissionDetails>>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<Pagination<PermissionDetails>>.Success(result)
            : Result<Pagination<PermissionDetails>>.Failure(SdkErrors.DeserializationFailure);
    }
}
