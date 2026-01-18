namespace Vinder.Federation.Sdk.Clients.Handlers;

public sealed class TenantHandler(IdentityProviderOptions options) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Remove("x-tenant");
        request.Headers.Add("x-tenant", options.Tenant);

        return await base.SendAsync(request, cancellationToken);
    }
}