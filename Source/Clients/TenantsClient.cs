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
}