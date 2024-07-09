using Microsoft.AspNetCore.Mvc;
using MultiTenantTest.Application.DTOs.Management.User;
using MultiTenantTest.Application.Services;
using MultiTenantTest.Domain.Models.Responses;

namespace MultiTenantTest.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<ServiceResult<LoginResponseDto>> LoginValidator(LoginDto LoginCredentials)
        {
            try
            {
                return await loginService.Login(LoginCredentials);
            }
            catch (Exception ex)
            {
                return ServiceResult<LoginResponseDto>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }
    }
}
