namespace Vinder.IdentityProvider.Sdk.Extensions;

public static class IdentityProviderExtension
{
    public static void AddIdentityProvider(this IServiceCollection services, Action<IdentityProviderOptions> configure)
    {
        var options = new IdentityProviderOptions();
        configure(options);

        services.AddTransient<OpenIDAuthenticationHandler>();
        services.AddTransient<TenantHandler>();

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