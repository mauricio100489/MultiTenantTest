using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Infrastructure.Context.Product;

namespace MultiTenantTest.Application.Repositories
{
    public class RepositoryGenericProductReader<TEntity> : RepositoryGeneric<TEntity, ProductContextReader> where TEntity : class
    {
        public RepositoryGenericProductReader(ProductContextReader context) : base(context)
        {
        }
    }
}