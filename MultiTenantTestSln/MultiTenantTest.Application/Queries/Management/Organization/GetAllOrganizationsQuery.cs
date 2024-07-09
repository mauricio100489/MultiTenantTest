using MediatR;
using MultiTenantTest.Application.DTOs.Management.Organization;

namespace MultiTenantTest.Application.Queries.Management.Organization
{
    public class GetAllOrganizationsQuery : IRequest<List<OrganizationDto>>
    {
    }
}
