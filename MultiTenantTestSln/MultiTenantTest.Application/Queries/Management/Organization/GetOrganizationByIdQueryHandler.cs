using MediatR;
using MultiTenantTest.Application.Exceptions;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Application.Shared.Management.Organization;

namespace MultiTenantTest.Application.Queries.Management.Organization
{
    public class GetOrganizationByIdQueryHandler : IRequestHandler<GetOrganizationByIdQuery, OrganizationDto>
    {
        private readonly IRepositoryGeneric<Domain.Models.Management.Organization> _repository;

        public GetOrganizationByIdQueryHandler(IRepositoryGeneric<Domain.Models.Management.Organization> repository)
        {
            _repository = repository;
        }

        public async Task<OrganizationDto> Handle(GetOrganizationByIdQuery request, CancellationToken cancellationToken)
        {
            var organization = await _repository.FirstOrDefaultAsync(item => item.Id == request.OrganizationId);

            if (organization == null)
            {
                throw new NotFoundException($"Organization with id {request.OrganizationId} not found.");
            }

            var organizationDto = new OrganizationDto
            {
                Id = organization.Id,
                Name = organization.Name
            };

            return organizationDto;
        }
    }
}