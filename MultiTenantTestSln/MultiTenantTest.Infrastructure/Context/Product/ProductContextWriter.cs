using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantTest.Domain.Models.Products.Writer;

namespace MultiTenantTest.Infrastructure.Context.Product
{
    public class ProductContextWriter : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ProductContextWriter(DbContextOptions<ProductContextWriter> options)
            : base(options)
        {
        }

        public ProductContextWriter(DbContextOptions<ProductContextWriter> options, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_httpContextAccessor != null && _configuration != null)
            {
                var tenant = _httpContextAccessor.HttpContext.Items["Tenant"]?.ToString();
                var connectionString = _configuration.GetConnectionString("ProductConnectionWriter").Replace("{tenant}", tenant);
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<ProductW> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductW>().ToTable("Products");
            modelBuilder.Entity<ProductW>().HasKey(p => p.Id);

        }
    }
}
