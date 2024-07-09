using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiTenantTest.Application.Commands.ManagementDatabase.User;
using MultiTenantTest.Application.Exceptions;
using MultiTenantTest.Application.Queries.Management.User;
using MultiTenantTest.Application.DTOs.Management.User;
using MultiTenantTest.Domain.Models.Responses;
using Microsoft.AspNetCore.Authorization;

namespace MultiTenantTest.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ServiceResult<List<UserDto>>> Get()
        {
            try
            {
                var Users = await _mediator.Send(new GetAllUsersQuery());

                return ServiceResult<List<UserDto>>.SuccessResult(Users);
            }
            catch (Exception ex)
            {
                return ServiceResult<List<UserDto>>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }

        [HttpGet("{id}")]
        public async Task<ServiceResult<UserDto>> GetUser(int id)
        {
            try
            {
                var query = new GetUserByIdQuery { UserId = id };
                var User = await _mediator.Send(query);
                
                return ServiceResult<UserDto>.SuccessResult(User);
            }
            catch(Exception ex)
            {
                return ServiceResult<UserDto>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }

        [HttpPost]
        public async Task<ServiceResult<UserDto>> CreateUser(CreateUserCommand command)
        {
            try
            {
                var UserDto = await _mediator.Send(command);

                return ServiceResult<UserDto>.SuccessResult(UserDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ServiceResult<UserDto>> DeleteUser(int id)
        {
            try
            {
                var command = new DeleteUserCommand { UserId = id };
                var UserDto = await _mediator.Send(command);

                return ServiceResult<UserDto>.SuccessResult(UserDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<UserDto>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }
    }
}