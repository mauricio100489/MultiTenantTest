using MediatR;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Domain.Models.Products.Reader;

namespace MultiTenantTest.Application.Commands.ProductsDatabase.Product.Reader
{
    public class ReplicateProductCommandHandler : IRequestHandler<ReplicateProductCommand, Unit>
    {
        private readonly IRepositoryGeneric<ProductR> repository;

        public ReplicateProductCommandHandler(IRepositoryGeneric<ProductR> repository)
        {
            this.repository = repository;
        }

        public async Task<Unit> Handle(ReplicateProductCommand command, CancellationToken cancellationToken)
        {
            var product = new ProductR
            {
                Name = command.Name,
                Description = command.Description,
            };

            repository.Create(product);
            await repository.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
