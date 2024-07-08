using MediatR;
using MultiTenantTest.Application.Exceptions;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Application.Services;
using MultiTenantTest.Application.Shared.Management.Organization;

namespace MultiTenantTest.Application.Commands.ManagementDatabase.User
{
    public class DeleteOrganizationCommandHandler : IRequestHandler<DeleteOrganizationCommand, OrganizationDto>
    {
        private readonly IRepositoryGeneric<Domain.Models.Management.Organization> _repository;
        private readonly IDatabaseCreationService databaseCreationService;

        public DeleteOrganizationCommandHandler(
            IRepositoryGeneric<Domain.Models.Management.Organization> repository,
            IDatabaseCreationService databaseCreationService)
        {
            _repository = repository;
            this.databaseCreationService = databaseCreationService;
        }

        public async Task<OrganizationDto> Handle(DeleteOrganizationCommand request, CancellationToken cancellationToken)
        {
            var organization = await _repository.FirstOrDefaultAsync(item => item.Id == request.OrganizationId);

            if (organization == null)
            {
                throw new NotFoundException($"Organization with id {request.OrganizationId} not found.");
            }

            _repository.Delete(organization);
            await _repository.SaveChangesAsync();

            // Si se desea eliminar tambien la db cuando se elimine la organización.
            // Se comenta debido a que usualmente se quiere dejar histórico.
            //await databaseCreationService.DeleteDatabaseAsync(organization.SlugTenant);
            
            var organizationDto = new OrganizationDto
            {
                Id = organization.Id,
                Name = organization.Name
            };

            return organizationDto;
        }
    }
}