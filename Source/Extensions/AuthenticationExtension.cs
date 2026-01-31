namespace Vinder.Federation.Sdk.Extensions;

public static class AuthenticationExtension
{
    public static void AddBearerAuthentication(this IServiceCollection services)
    {
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<FederationOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(configuration =>
            {
                configuration.Authority = options.BaseUrl;
                configuration.Audience = options.Tenant;
                configuration.RequireHttpsMetadata = false;
            });
    }
}