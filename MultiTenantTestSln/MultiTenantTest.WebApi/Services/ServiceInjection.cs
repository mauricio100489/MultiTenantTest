using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MultiTenantTest.Application.Commands.ProductsDatabase.Product.Writer;
using MultiTenantTest.Application.Queries.ProductsDatabase.Product;
using MultiTenantTest.Application.Repositories;
using MultiTenantTest.Application.Repositories.Configuration;
using MultiTenantTest.Application.Services;
using MultiTenantTest.Domain.Models.Management;
using MultiTenantTest.Domain.Models.Products.Reader;
using MultiTenantTest.Domain.Models.Products.Writer;
using System.Text;

namespace MultiTenantTest.WebAPI.Services
{
    public static class ServiceInjection
    {
        public static void AddCustomInjections(this IServiceCollection services)
        {

            AddCustomServices(services);
            AddCustomRepositories(services);
            AddCommands(services);
            AddQueries(services);
        }

        public static IServiceCollection AddCustomServices(IServiceCollection services)
        {
            services.AddScoped<IDatabaseCreationService, DatabaseCreationService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "yourissuer",
                            ValidAudience = "youraudience",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
                        };
                    });
            //services.AddScoped<IMyCustomService, MyCustomService>();

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
