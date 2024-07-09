using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MultiTenantTest.Application.Commands.ProductsDatabase.Product.Writer;
using MultiTenantTest.Application.Queries.ProductsDatabase.Product;
using MultiTenantTest.Application.Repositories;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Application.Services;
using MultiTenantTest.Domain.Entities.Management;
using MultiTenantTest.Domain.Entities.Products.Reader;
using MultiTenantTest.Domain.Entities.Products.Writer;
using System.Text;

namespace MultiTenantTest.WebAPI.Services
{
    public static class ServiceInjection
    {
        public static void AddCustomInjections(this IServiceCollection services, IConfiguration configuration)
        {
            AddCurtomService(services);
            AddJwtAuth(services, configuration);
            AddCustomRepositories(services);
            AddCommands(services);
            AddQueries(services);
        }

        public static IServiceCollection AddCurtomService(IServiceCollection services)
        {
            services.AddScoped<IDatabaseCreationService, DatabaseCreationService>();
            services.AddScoped<ILoginService, LoginService>();

            return services;
        }

        public static IServiceCollection AddJwtAuth(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                };
            });

            return services;
        }

        public static IServiceCollection AddCustomRepositories(IServiceCollection services)
        {
            services.AddScoped<IRepositoryGeneric<ProductR>, RepositoryGenericProductReader<ProductR>>();
            services.AddScoped<IRepositoryGeneric<ProductW>, RepositoryGenericProductWriter<ProductW>>();
            services.AddScoped<IRepositoryGeneric<Organization>, RepositoryGenericManagement<Organization>>();
            services.AddScoped<IRepositoryGeneric<User>, RepositoryGenericManagement<User>>();

            return services;
        }

        public static IServiceCollection AddCommands(IServiceCollection services)
        {
            services.AddScoped<CreateProductCommandHandler>();
            return services;
        }

        public static IServiceCollection AddQueries(IServiceCollection services)
        {
            services.AddScoped<GetAllProductsQueryHandler>();
            return services;
        }
    }
}
