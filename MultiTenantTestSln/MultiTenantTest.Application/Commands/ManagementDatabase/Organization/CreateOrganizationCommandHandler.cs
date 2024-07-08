using MediatR;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Application.Services;
using MultiTenantTest.Application.Shared.Management.Organization;

namespace MultiTenantTest.Application.Commands.ManagementDatabase.User
{
    public class CreateOrganizationCommandHandler : IRequestHandler<CreateOrganizationCommand, OrganizationDto>
    {
        private readonly IRepositoryGeneric<Domain.Models.Management.Organization> _repository;
        private readonly IDatabaseCreationService databaseCreationService;

        public CreateOrganizationCommandHandler(
            IRepositoryGeneric<Domain.Models.Management.Organization> repository,
            IDatabaseCreationService databaseCreationService)
        {
            _repository = repository;
            this.databaseCreationService = databaseCreationService;
        }

        public async Task<OrganizationDto> Handle(CreateOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = new Domain.Models.Management.Organization
            {
                Name = request.Name,
                CreatedDateTimeOffset = DateTimeOffset.UtcNow,
                SlugTenant = request.SlugTenant,
            };

            _repository.Create(organization);
            await _repository.SaveChangesAsync();

            await databaseCreationService.CreateDatabaseAsync(organization.SlugTenant);

            var organizationDto = new OrganizationDto
            {
                Id = organization.Id,
                Name = organization.Name,
            };

            return organizationDto;
        }
    }
}