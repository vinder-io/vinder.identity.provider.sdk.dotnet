namespace Vinder.Federation.Sdk.Extensions;

public static class FederationExtension
{
    public static void AddFederation(this IServiceCollection services, Action<FederationOptions> configure)
    {
        var options = new FederationOptions();

        configure(options);

        services.AddTransient<AuthenticationInterceptor>();
        services.AddTransient<TenantInterceptor>();

        services.AddSingleton(options);
        services.AddHttpClient<IOpenIDConnectClient, OpenIDConnectClient>(client =>
        {
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddSdkHttpClient<IIdentityClient, IdentityClient>(options.BaseUrl)
            .WithTenant()
            .WithAuthentication();

        services.AddSdkHttpClient<IPermissionsClient, PermissionsClient>(options.BaseUrl)
            .WithTenant()
            .WithAuthentication();

        services.AddSdkHttpClient<IGroupsClient, GroupsClient>(options.BaseUrl)
            .WithTenant()
            .WithAuthentication();

        services.AddSdkHttpClient<ITenantsClient, TenantsClient>(options.BaseUrl)
            .WithTenant()
            .WithAuthentication();

        services.AddSdkHttpClient<IUsersClient, UsersClient>(options.BaseUrl)
            .WithTenant()
            .WithAuthentication();

        services.AddBearerAuthentication();
        services.AddAuthorization();
    }
}