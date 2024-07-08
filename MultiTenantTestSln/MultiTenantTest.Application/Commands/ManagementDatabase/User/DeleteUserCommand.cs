using MediatR;
using MultiTenantTest.Application.Shared.Management.User;

namespace MultiTenantTest.Application.Commands.ManagementDatabase.User
{
    public class DeleteUserCommand : IRequest<UserDto>
    {
        public int UserId { get; set; }
    }
}