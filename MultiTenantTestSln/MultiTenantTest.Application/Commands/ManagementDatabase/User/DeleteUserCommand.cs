using MediatR;
using MultiTenantTest.Application.DTOs.Management.User;

namespace MultiTenantTest.Application.Commands.ManagementDatabase.User
{
    public class DeleteUserCommand : IRequest<UserDto>
    {
        public int UserId { get; set; }
    }
}