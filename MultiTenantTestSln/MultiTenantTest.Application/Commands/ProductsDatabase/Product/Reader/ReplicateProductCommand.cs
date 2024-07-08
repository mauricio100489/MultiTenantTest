using MediatR;

namespace MultiTenantTest.Application.Commands.ProductsDatabase.Product.Reader
{
    public class ReplicateProductCommand : IRequest<Unit>
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ReplicateProductCommand(int productId, string name, string description)
        {
            ProductId = productId;
            Name = name;
            Description = description;
        }
    }
}
