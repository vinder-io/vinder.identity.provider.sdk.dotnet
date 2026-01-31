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
        services.AddHttpClient<IConnectClient, ConnectClient>(client =>
        {
            client.BaseAddress = new Uri(options.BaseUrl);
        });

        services.AddSdkHttpClient<IIdentityClient, IdentityClient>(options.BaseUrl)
            .WithTenantInterceptor()
            .WithAuthenticationInterceptor();

        services.AddSdkHttpClient<IPermissionsClient, PermissionsClient>(options.BaseUrl)
            .WithTenantInterceptor()
            .WithAuthenticationInterceptor();

        services.AddSdkHttpClient<IGroupsClient, GroupsClient>(options.BaseUrl)
            .WithTenantInterceptor()
            .WithAuthenticationInterceptor();

        services.AddSdkHttpClient<ITenantsClient, TenantsClient>(options.BaseUrl)
            .WithTenantInterceptor()
            .WithAuthenticationInterceptor();

        services.AddSdkHttpClient<IUsersClient, UsersClient>(options.BaseUrl)
            .WithTenantInterceptor()
            .WithAuthenticationInterceptor();

        services.AddBearerAuthentication();
        services.AddAuthorization();
    }
}