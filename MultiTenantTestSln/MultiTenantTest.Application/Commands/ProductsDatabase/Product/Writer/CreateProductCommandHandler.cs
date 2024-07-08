using MediatR;
using MultiTenantTest.Application.Commands.ProductsDatabase.Product.Reader;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Domain.Models.Products.Writer;

namespace MultiTenantTest.Application.Commands.ProductsDatabase.Product.Writer
{
    public class CreateProductCommandHandler
    {
        private readonly IRepositoryGeneric<ProductW> repository;
        private readonly IMediator mediator;

        public CreateProductCommandHandler(
            IRepositoryGeneric<ProductW> repository,
            IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        public async Task Handle(CreateProductCommand command)
        {
            var product = new ProductW
            {
                Name = command.Name,
                Description = command.Description,
                CreatedDateTimeOffset = DateTimeOffset.UtcNow,
            };

            repository.Create(product);
            await repository.SaveChangesAsync();

            var replicateCommand = new ReplicateProductCommand(
                product.Id,
                product.Name,
                product.Description);

            await mediator.Send(replicateCommand);
        }
    }
}
