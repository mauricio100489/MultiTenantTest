using MediatR;
using MultiTenantTest.Application.Shared.Management.Organization;

namespace MultiTenantTest.Application.Queries.Management.Organization
{
    public class GetOrganizationByIdQuery : IRequest<OrganizationDto>
    {
        public int OrganizationId { get; set; }
    }
}