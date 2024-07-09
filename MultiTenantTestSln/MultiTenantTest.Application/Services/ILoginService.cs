using MultiTenantTest.Application.DTOs.Management.User;
using MultiTenantTest.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantTest.Application.Services
{
    public interface ILoginService
    {
        Task<ServiceResult<LoginResponseDto>> Login(LoginDto loginData);
    }
}
