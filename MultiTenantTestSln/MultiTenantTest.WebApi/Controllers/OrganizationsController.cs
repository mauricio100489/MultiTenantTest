using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiTenantTest.Application.Commands.ManagementDatabase.User;
using MultiTenantTest.Application.Exceptions;
using MultiTenantTest.Application.Queries.Management.Organization;
using MultiTenantTest.Application.Shared.Management.Organization;

namespace MultiTenantTest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<OrganizationDto>> GetOrganization(int id)
        {
            try
            {
                var query = new GetOrganizationByIdQuery { OrganizationId = id };
                var organization = await _mediator.Send(query);
                return Ok(organization);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrganization(CreateOrganizationCommand command)
        {
            var organizationDto = await _mediator.Send(command);
            return Ok(organizationDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OrganizationDto>> DeleteOrganization(int id)
        {
            try
            {
                var command = new DeleteOrganizationCommand { OrganizationId = id };
                var organizationDto = await _mediator.Send(command);
                return Ok(organizationDto);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}