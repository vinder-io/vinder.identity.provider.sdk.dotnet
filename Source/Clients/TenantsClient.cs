namespace Vinder.IdentityProvider.Sdk.Clients;

public sealed class TenantsClient(HttpClient httpClient) : ITenantsClient
{
    public async Task<Result<Pagination<TenantDetails>>> GetTenantsAsync(
        TenantFetchParameters parameters, CancellationToken cancellation = default)
    {
        string queryString = QueryParametersParser.ToQueryString(parameters);
        string url = $"api/v1/tenants?{queryString}";

        var response = await httpClient.GetAsync(url, cancellation);
        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<Pagination<TenantDetails>>.Failure(error)
                : Result<Pagination<TenantDetails>>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<Pagination<TenantDetails>>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<Pagination<TenantDetails>>.Success(result)
            : Result<Pagination<TenantDetails>>.Failure(SdkErrors.DeserializationFailure);
    }

    public async Task<Result<TenantDetails>> CreateTenantAsync(TenantCreationScheme tenant, CancellationToken cancellation = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/v1/tenants", tenant, cancellation);
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result<TenantDetails>.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<TenantDetails>.Failure(error)
                : Result<TenantDetails>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<TenantDetails>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<TenantDetails>.Success(result)
            : Result<TenantDetails>.Failure(SdkErrors.DeserializationFailure);
    }

    public async Task<Result<TenantDetails>> UpdateTenantAsync(TenantUpdateScheme tenant, CancellationToken cancellation = default)
    {
        var response = await httpClient.PutAsJsonAsync($"api/v1/tenants/{tenant.TenantId}", tenant, cancellation);
        if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
        {
            return Result<TenantDetails>.Failure(SdkErrors.Unauthorized);
        }

        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<TenantDetails>.Failure(error)
                : Result<TenantDetails>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<TenantDetails>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<TenantDetails>.Success(result)
            : Result<TenantDetails>.Failure(SdkErrors.DeserializationFailure);
    }

    public async Task<Result> DeleteTenantAsync(string tenantId, CancellationToken cancellation = default)
    {
        var response = await httpClient.DeleteAsync($"api/v1/tenants/{tenantId}", cancellation);
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