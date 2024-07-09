using MediatR;
using MultiTenantTest.Application.DTOs.Management.User;

namespace MultiTenantTest.Application.Commands.ManagementDatabase.User
{
    public class CreateUserCommand : IRequest<UserDto>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
    }
}