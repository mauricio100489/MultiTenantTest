using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiTenantTest.Application.Commands.ManagementDatabase.User;
using MultiTenantTest.Application.Queries.Management.Organization;
using MultiTenantTest.Application.DTOs.Management.Organization;
using MultiTenantTest.Domain.Models.Responses;
using Microsoft.AspNetCore.Authorization;

namespace MultiTenantTest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrganizationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrganizationDto>>> Get()
        {
            var organizations = await _mediator.Send(new GetAllOrganizationsQuery());
            return Ok(organizations);
        }

        [HttpGet("{id}")]
        public async Task<ServiceResult<OrganizationDto>> GetOrganization(int id)
        {
            try
            {
                var query = new GetOrganizationByIdQuery { OrganizationId = id };
                var organization = await _mediator.Send(query);
                
                return ServiceResult<OrganizationDto>.SuccessResult(organization);
            }
            catch (Exception ex)
            {
                return ServiceResult<OrganizationDto>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ServiceResult<OrganizationDto>> CreateOrganization(CreateOrganizationCommand command)
        {
            try
            {
                var organizationDto = await _mediator.Send(command);

                return ServiceResult<OrganizationDto>.SuccessResult(organizationDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<OrganizationDto>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ServiceResult<OrganizationDto>> DeleteOrganization(int id)
        {
            try
            {
                var command = new DeleteOrganizationCommand { OrganizationId = id };
                var organizationDto = await _mediator.Send(command);

                return ServiceResult<OrganizationDto>.SuccessResult(organizationDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<OrganizationDto>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }
    }
}