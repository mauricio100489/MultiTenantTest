using Microsoft.EntityFrameworkCore;
using MultiTenantTest.Application.Queries.Management.Organization;
using MultiTenantTest.Infrastructure.Context.Management;
using MultiTenantTest.Infrastructure.Context.Product;
using MultiTenantTest.WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllOrganizationsQueryHandler).Assembly));

builder.Services.AddDbContext<ManagementContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ProductContextWriter>();
builder.Services.AddDbContext<ProductContextReader>();
// Add services to the container.
builder.Services.AddCustomInjections();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<TenantMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    var restartIMCDatabsae = false;
    await app.InitializerService(restartIMCDatabsae);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
