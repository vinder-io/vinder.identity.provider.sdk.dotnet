namespace Vinder.IdentityProvider.Sdk.Clients;

public sealed class IdentityClient(HttpClient httpClient) : IIdentityClient
{
    public async Task<Result<AuthenticationResult>> AuthenticateAsync(
        AuthenticationCredentials credentials, CancellationToken cancellation = default
    )
    {
        var response = await httpClient.PostAsJsonAsync("api/v1/identity/authenticate", credentials, cancellation);
        if (response.IsSuccessStatusCode is false)
        {
            var error = await response.Content.ReadFromJsonAsync<Error>(
                options: JsonSerialization.SerializerOptions,
                cancellationToken: cancellation
            );

            return error is not null
                ? Result<AuthenticationResult>.Failure(error)
                : Result<AuthenticationResult>.Failure(SdkErrors.DeserializationFailure);
        }

        var result = await response.Content.ReadFromJsonAsync<AuthenticationResult>(
            options: JsonSerialization.SerializerOptions,
            cancellationToken: cancellation
        );

        return result is not null
            ? Result<AuthenticationResult>.Success(result)
            : Result<AuthenticationResult>.Failure(SdkErrors.DeserializationFailure);
    }

    public async Task<Result> CreateIdentityAsync(IdentityEnrollmentCredentials credentials, CancellationToken cancellation = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/v1/identity", credentials, cancellation);
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