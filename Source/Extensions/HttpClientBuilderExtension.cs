namespace Vinder.Federation.Sdk.Extensions;

public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder AddSdkHttpClient<TClient, TImplementation>(this IServiceCollection services, string baseUrl)
        where TClient : class
        where TImplementation : class, TClient
    {
        return services.AddHttpClient<TClient, TImplementation>(client =>
        {
            client.BaseAddress = new Uri(baseUrl);
        });
    }

    public static IHttpClientBuilder WithTenant(this IHttpClientBuilder builder)
    {
        return builder.AddHttpMessageHandler<TenantHandler>();
    }

    public static IHttpClientBuilder WithAuthentication(this IHttpClientBuilder builder)
    {
        return builder.AddHttpMessageHandler<OpenIDAuthenticationHandler>();
    }
}
