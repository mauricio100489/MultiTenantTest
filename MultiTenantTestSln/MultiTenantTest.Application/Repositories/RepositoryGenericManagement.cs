using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Infrastructure.Context.Management;

namespace MultiTenantTest.Application.Repositories
{
    public class RepositoryGenericManagement<TEntity> : RepositoryGeneric<TEntity, ManagementContext> where TEntity : class
    {
        public RepositoryGenericManagement(ManagementContext context) : base(context)
        {
        }
    }
}