namespace Vinder.IdentityProvider.Sdk.Contracts;

public interface ITenantsClient
{
    public Task<Result<Pagination<TenantDetails>>> GetTenantsAsync(
        TenantFetchParameters parameters,
        CancellationToken cancellation = default
    );
}