using MediatR;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Application.Shared.Management.User;
using MultiTenantTest.Domain.Models.Management;

namespace MultiTenantTest.Application.Commands.ManagementDatabase.User
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IRepositoryGeneric<Domain.Models.Management.User> _repository;
        private readonly IRepositoryGeneric<Organization> organizationRepository;

        public CreateUserCommandHandler(IRepositoryGeneric<Domain.Models.Management.User> repository,
            IRepositoryGeneric<Domain.Models.Management.Organization> organizationRepository)
        {
            _repository = repository;
            this.organizationRepository = organizationRepository;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var User = new Domain.Models.Management.User
            {
                Email = request.Email,
                OrganizationId = request.OrganizationId,
                Password = request.Password,
                CreatedDateTimeOffset = DateTimeOffset.UtcNow
            };

            _repository.Create(User);
            await _repository.SaveChangesAsync();

            var result = await organizationRepository.FirstOrDefaultAsync(o => o.Id == User.OrganizationId);

            var UserDto = new UserDto
            {
                Id = User.Id,
                Email = User.Email,
                Organization = result.Name,
                OrganizationId = User.OrganizationId,
            };

            return UserDto;
        }
    }
}