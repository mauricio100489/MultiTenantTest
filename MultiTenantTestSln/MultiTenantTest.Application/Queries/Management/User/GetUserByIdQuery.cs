using MediatR;
using MultiTenantTest.Application.Exceptions;
using MultiTenantTest.Application.Repositories.Configuration;
using Microsoft.EntityFrameworkCore;
using MultiTenantTest.Application.DTOs.Management.User;

namespace MultiTenantTest.Application.Queries.Management.User
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int UserId { get; set; }
    }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IRepositoryGeneric<Domain.Entities.Management.User> _repository;

        public GetUserByIdQueryHandler(IRepositoryGeneric<Domain.Entities.Management.User> repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var User = await _repository.All()
                .Include(user => user.Organization)
                .FirstOrDefaultAsync(item => item.Id == request.UserId);

            if (User == null)
            {
                throw new NotFoundException($"User with id {request.UserId} not found.");
            }

            var UserDto = new UserDto
            {
                Id = User.Id,
                Email = User.Email,
                Organization = User.Organization!.Name,
                OrganizationId = User.OrganizationId,
            };

            return UserDto;
        }
    }
}