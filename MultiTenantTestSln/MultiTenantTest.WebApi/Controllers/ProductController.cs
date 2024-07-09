using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantTest.Application.Commands.ProductsDatabase.Product.Writer;
using MultiTenantTest.Application.Queries.ProductsDatabase.Product;
using MultiTenantTest.Domain.Entities.Products.Reader;
using MultiTenantTest.Domain.Models.Responses;

namespace MultiTenantTest.WebAPI.Controllers
{
    [ApiController]
    [Route("{tenant}/api/products")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly CreateProductCommandHandler _createProductHandler;
        private readonly GetAllProductsQueryHandler _getAllProductsHandler;

        public ProductController(CreateProductCommandHandler createProductHandler,
                                 GetAllProductsQueryHandler getAllProductsHandler)
        {
            _createProductHandler = createProductHandler;
            _getAllProductsHandler = getAllProductsHandler;
        }

        [HttpPost]
        public async Task<ServiceResult<bool>> CreateProduct(CreateProductCommand command)
        {
            try
            {
                await _createProductHandler.Handle(command);
                return ServiceResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<ServiceResult<List<ProductR>>> GetAllProducts()
        {
            try
            {
                var query = new GetAllProductsQuery();
                var products = await _getAllProductsHandler.Handle(query);
                
                return ServiceResult<List<ProductR>>.SuccessResult(products);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<ProductR>>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }
    }
}
