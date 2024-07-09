using MediatR;
using MultiTenantTest.Application.DTOs.Management.Organization;

namespace MultiTenantTest.Application.Commands.ManagementDatabase.User
{
    public class CreateOrganizationCommand : IRequest<OrganizationDto>
    {
        public string Name { get; set; } = string.Empty;
        public string SlugTenant { get; set; } = string.Empty;
    }
}
