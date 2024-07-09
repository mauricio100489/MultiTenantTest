using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MultiTenantTest.Application.DTOs.Management.User;
using MultiTenantTest.Application.Repositories;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Domain.Entities.Management;
using MultiTenantTest.Domain.Models.Responses;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MultiTenantTest.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IRepositoryGeneric<User> repositoryUser;
        private readonly IRepositoryGeneric<Organization> repositoryOrganization;
        private readonly IConfiguration configuration;

        public LoginService(
            IRepositoryGeneric<User> repositoryUser,
            IRepositoryGeneric<Organization> repositoryOrganization,
            IConfiguration configuration
            )
        {
            this.repositoryUser = repositoryUser;
            this.repositoryOrganization = repositoryOrganization;
            this.configuration = configuration;
        }

        public async Task<ServiceResult<LoginResponseDto>> Login(LoginDto LoginData)
        {
            try
            {
                var user = await this.repositoryUser
                                    .All()
                                    .Where(c => c.Email == LoginData.Email && c.Password == LoginData.Password)
                                    .FirstOrDefaultAsync();

                if (user == null)
                    return ServiceResult<LoginResponseDto>.ErrorResult(new[] { $"Credenciales Incorrectas." });

                LoginResponseDto loginResponse = new()
                {
                    AccessToken = await GenerateTokenAsync(user),
                    Tenants = await repositoryOrganization
                                        .All()
                                        .Where(c => c.Id == user.OrganizationId)
                                        .Select(c => new Tenants
                                        {
                                            SlugTenant = c.SlugTenant
                                        })
                                        .ToListAsync()
                };

                return ServiceResult<LoginResponseDto>.SuccessResult(loginResponse);
            }
            catch (Exception ex) 
            {
                return ServiceResult<LoginResponseDto>.ErrorResult(new[] { $"{ex.Message}" });
            }
        }

        private async Task<string> GenerateTokenAsync(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim("email", user.Email ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(configuration["Jwt:MinuteToExpires"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
