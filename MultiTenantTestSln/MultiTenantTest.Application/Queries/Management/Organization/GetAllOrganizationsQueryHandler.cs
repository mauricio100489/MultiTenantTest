using MediatR;
using Microsoft.EntityFrameworkCore;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Application.Shared.Management.Organization;

namespace MultiTenantTest.Application.Queries.Management.Organization
{
    public class GetAllOrganizationsQueryHandler : IRequestHandler<GetAllOrganizationsQuery, List<OrganizationDto>>
    {
        private readonly IRepositoryGeneric<Domain.Models.Management.Organization> repository;

        public GetAllOrganizationsQueryHandler(IRepositoryGeneric<Domain.Models.Management.Organization> repository)
        {
            this.repository = repository;
        }

        public async Task<List<OrganizationDto>> Handle(GetAllOrganizationsQuery request, CancellationToken cancellationToken)
        {
            var organizations = await repository.All()
                .Select(o => new OrganizationDto
                {
                    Id = o.Id,
                    Name = o.Name
                })
                .ToListAsync(cancellationToken);

            return organizations;
        }
    }
}
