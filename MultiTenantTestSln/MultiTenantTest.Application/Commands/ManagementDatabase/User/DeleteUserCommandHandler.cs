using MediatR;
using Microsoft.EntityFrameworkCore;
using MultiTenantTest.Application.Exceptions;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Application.DTOs.Management.User;

namespace MultiTenantTest.Application.Commands.ManagementDatabase.User
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserDto>
    {
        private readonly IRepositoryGeneric<Domain.Entities.Management.User> _repository;

        public DeleteUserCommandHandler(IRepositoryGeneric<Domain.Entities.Management.User> repository)
        {
            _repository = repository;
        }

        public async Task<UserDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var User = await _repository.All()
                .Include(o => o.Organization)
                .FirstOrDefaultAsync(item => item.Id == request.UserId);

            if (User == null)
            {
                throw new NotFoundException($"User with id {request.UserId} not found.");
            }

            _repository.Delete(User);
            await _repository.SaveChangesAsync();

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