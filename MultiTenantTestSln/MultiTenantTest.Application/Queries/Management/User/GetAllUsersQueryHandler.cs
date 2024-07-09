using MediatR;
using MultiTenantTest.Application.Repositories.Configuration;
using Microsoft.EntityFrameworkCore;
using MultiTenantTest.Application.DTOs.Management.User;

namespace MultiTenantTest.Application.Queries.Management.User
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IRepositoryGeneric<Domain.Entities.Management.User> repository;

        public GetAllUsersQueryHandler(IRepositoryGeneric<Domain.Entities.Management.User> repository)
        {
            this.repository = repository;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await repository.All()
                .Include(user => user.Organization)
                .Select(o => new UserDto
                {
                    Id = o.Id,
                    Email = o.Email,
                    Organization = o.Organization!.Name,
                    OrganizationId = o.OrganizationId,
                })
                .ToListAsync(cancellationToken);

            return users;
        }
    }
}
