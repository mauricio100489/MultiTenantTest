using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MultiTenantTest.Application.Queries.Management.Organization;
using MultiTenantTest.Infrastructure.Context.Management;
using MultiTenantTest.Infrastructure.Context.Product;
using MultiTenantTest.WebAPI.Services;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllOrganizationsQueryHandler).Assembly));

// Add DBContext
builder.Services.AddDbContext<ManagementContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ProductContextWriter>();
builder.Services.AddDbContext<ProductContextReader>();

// Add services to the container.
builder.Services.AddCustomInjections(builder.Configuration);

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add authentication to Swagger UI
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

app.UseMiddleware<TenantMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    await app.InitializerService(false);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
