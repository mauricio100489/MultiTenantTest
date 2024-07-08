namespace MultiTenantTest.Application.Services
{
    public interface IDatabaseCreationService
    {
        Task CreateDatabaseAsync(string tenantSlug);
        Task DeleteDatabaseAsync(string tenantSlug);
    }
}