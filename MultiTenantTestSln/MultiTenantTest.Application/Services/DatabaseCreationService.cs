using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantTest.Infrastructure.Context.Product;

namespace MultiTenantTest.Application.Services
{
    public class DatabaseCreationService : IDatabaseCreationService
    {
        private readonly IConfiguration _configuration;

        public DatabaseCreationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task CreateDatabaseAsync(string tenantSlug)
        {
            await CreateWriterDatabase(tenantSlug);
            await CreateReaderDatabase(tenantSlug);
        }

        public async Task DeleteDatabaseAsync(string tenantSlug)
        {
            await DeleteWriterDatabase(tenantSlug);
            await DeleteReaderDatabase(tenantSlug);
        }

        private async Task DeleteWriterDatabase(string tenantSlug)
        {
            var connectionStringTemplate = _configuration.GetConnectionString("ProductConnectionWriter");
            var connectionString = connectionStringTemplate.Replace("{tenant}", tenantSlug);

            var optionsBuilder = new DbContextOptionsBuilder<ProductContextWriter>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new ProductContextWriter(optionsBuilder.Options, null, _configuration))
            {
                await context.Database.EnsureDeletedAsync();
            }
        }

        private async Task CreateWriterDatabase(string tenantSlug)
        {
            var connectionStringTemplate = _configuration.GetConnectionString("ProductConnectionWriter");
            var connectionString = connectionStringTemplate.Replace("{tenant}", tenantSlug);

            var optionsBuilder = new DbContextOptionsBuilder<ProductContextWriter>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new ProductContextWriter(optionsBuilder.Options, null, _configuration))
            {
                await context.Database.EnsureCreatedAsync();
            }
        }

        private async Task DeleteReaderDatabase(string tenantSlug)
        {
            var connectionStringTemplate = _configuration.GetConnectionString("ProductConnectionReader");
            var connectionString = connectionStringTemplate.Replace("{tenant}", tenantSlug);

            var optionsBuilder = new DbContextOptionsBuilder<ProductContextReader>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new ProductContextReader(optionsBuilder.Options, null, _configuration))
            {
                await context.Database.EnsureDeletedAsync();
            }
        }
        private async Task CreateReaderDatabase(string tenantSlug)
        {
            var connectionStringTemplate = _configuration.GetConnectionString("ProductConnectionReader");
            var connectionString = connectionStringTemplate.Replace("{tenant}", tenantSlug);

            var optionsBuilder = new DbContextOptionsBuilder<ProductContextReader>();
            optionsBuilder.UseSqlServer(connectionString);

            using (var context = new ProductContextReader(optionsBuilder.Options, null, _configuration))
            {
                await context.Database.EnsureCreatedAsync();
            }
        }
    }
}