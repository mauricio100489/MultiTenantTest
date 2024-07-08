using Microsoft.AspNetCore.Mvc;
using MultiTenantTest.Application.Commands.ProductsDatabase.Product.Writer;
using MultiTenantTest.Application.Queries.ProductsDatabase.Product;

namespace MultiTenantTest.WebAPI.Controllers
{
    [ApiController]
    [Route("{tenant}/api/products")]
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
        public async Task<IActionResult> CreateProduct(CreateProductCommand command)
        {
            await _createProductHandler.Handle(command);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var products = await _getAllProductsHandler.Handle(query);
            return Ok(products);
        }
    }
}
