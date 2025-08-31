namespace Vinder.IdentityProvider.Sdk.Extensions;

public static class AuthenticationExtension
{
    public static void AddBearerAuthentication(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IdentityProviderOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(configuration =>
            {
                configuration.Authority = options.BaseUrl;
                configuration.Audience = options.Tenant;
            });
    }
}