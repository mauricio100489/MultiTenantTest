using MediatR;
using MultiTenantTest.Application.Shared.Management.Organization;

namespace MultiTenantTest.Application.Commands.ManagementDatabase.User
{
    public class DeleteOrganizationCommand : IRequest<OrganizationDto>
    {
        public int OrganizationId { get; set; }
    }
}