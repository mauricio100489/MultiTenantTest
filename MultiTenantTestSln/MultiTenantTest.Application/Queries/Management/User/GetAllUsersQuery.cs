using MediatR;
using MultiTenantTest.Application.Shared.Management.User;

namespace MultiTenantTest.Application.Queries.Management.User
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
    }
}
