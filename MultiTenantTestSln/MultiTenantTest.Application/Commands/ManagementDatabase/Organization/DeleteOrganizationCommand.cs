using MediatR;
using MultiTenantTest.Application.DTOs.Management.Organization;

namespace MultiTenantTest.Application.Commands.ManagementDatabase.User
{
    public class DeleteOrganizationCommand : IRequest<OrganizationDto>
    {
        public int OrganizationId { get; set; }
    }
}