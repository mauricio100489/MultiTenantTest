using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultiTenantTest.Domain.Entities.Management;

namespace MultiTenantTest.Infrastructure.Context.Management
{
    public class ManagementContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ManagementContext(DbContextOptions<ManagementContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        // DbSets
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Organization>().ToTable("Organizations");
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Organization>().HasKey(o => o.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            modelBuilder.Entity<Organization>()
                .HasMany<User>()
                .WithOne(u => u.Organization)
                .HasForeignKey(u => u.OrganizationId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
