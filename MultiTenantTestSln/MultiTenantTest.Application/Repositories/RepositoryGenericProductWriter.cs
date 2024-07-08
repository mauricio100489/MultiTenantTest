using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Infrastructure.Context.Product;

namespace MultiTenantTest.Application.Repositories
{
    public class RepositoryGenericProductWriter<TEntity> : RepositoryGeneric<TEntity, ProductContextWriter> where TEntity : class
    {
        public RepositoryGenericProductWriter(ProductContextWriter context) : base(context)
        {
        }
    }
}