using MediatR;
using MultiTenantTest.Application.DTOs.Management.User;

namespace MultiTenantTest.Application.Queries.Management.User
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
    }
}
