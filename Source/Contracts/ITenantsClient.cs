namespace Vinder.IdentityProvider.Sdk.Contracts;

public interface ITenantsClient
{
    public Task<Result<Pagination<TenantDetails>>> GetTenantsAsync(
        TenantFetchParameters parameters,
        CancellationToken cancellation = default
    );

    public Task<Result<TenantDetails>> CreateTenantAsync(
        TenantForCreation tenant,
        CancellationToken cancellation = default
    );

    public Task<Result<TenantDetails>> UpdateTenantAsync(
        TenantForUpdate tenant,
        CancellationToken cancellation = default
    );

    public Task<Result> DeleteTenantAsync(
        Guid tenantId,
        CancellationToken cancellation = default
    );
}