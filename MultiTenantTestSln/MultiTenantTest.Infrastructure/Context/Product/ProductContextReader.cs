using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantTest.Domain.Entities.Products.Reader;

namespace MultiTenantTest.Infrastructure.Context.Product
{
    public class ProductContextReader : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ProductContextReader(DbContextOptions<ProductContextReader> options)
            : base(options)
        {
        }

        public ProductContextReader(DbContextOptions<ProductContextReader> options, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
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
                var connectionString = _configuration.GetConnectionString("ProductConnectionReader").Replace("{tenant}", tenant);
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<ProductR> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductR>().ToTable("Products");
            modelBuilder.Entity<ProductR>().HasKey(p => p.Id);
        }
    }
}
