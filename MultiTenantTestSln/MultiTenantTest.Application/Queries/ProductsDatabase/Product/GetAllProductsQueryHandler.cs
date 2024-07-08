using Microsoft.EntityFrameworkCore;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Domain.Models.Products.Reader;

namespace MultiTenantTest.Application.Queries.ProductsDatabase.Product
{
    public class GetAllProductsQueryHandler
    {
        private readonly IRepositoryGeneric<ProductR> repository;

        public GetAllProductsQueryHandler(IRepositoryGeneric<ProductR> repository)
        {
            this.repository = repository;
        }

        public async Task<List<ProductR>> Handle(GetAllProductsQuery query)
        {
            return await repository.All().ToListAsync();
        }
    }
}
