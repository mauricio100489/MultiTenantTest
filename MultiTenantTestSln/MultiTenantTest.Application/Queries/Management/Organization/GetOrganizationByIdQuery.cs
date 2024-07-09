using MediatR;
using MultiTenantTest.Application.DTOs.Management.Organization;

namespace MultiTenantTest.Application.Queries.Management.Organization
{
    public class GetOrganizationByIdQuery : IRequest<OrganizationDto>
    {
        public int OrganizationId { get; set; }
    }
}