using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiTenantTest.Application.Commands.ManagementDatabase.User;
using MultiTenantTest.Application.Exceptions;
using MultiTenantTest.Application.Queries.Management.User;
using MultiTenantTest.Application.Shared.Management.User;

namespace MultiTenantTest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            var Users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            try
            {
                var query = new GetUserByIdQuery { UserId = id };
                var User = await _mediator.Send(query);
                return Ok(User);
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
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var UserDto = await _mediator.Send(command);
            return Ok(UserDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> DeleteUser(int id)
        {
            try
            {
                var command = new DeleteUserCommand { UserId = id };
                var UserDto = await _mediator.Send(command);
                return Ok(UserDto);
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