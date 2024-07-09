using MultiTenantTest.Infrastructure.Context.Management;

namespace MultiTenantTest.WebAPI.Services
{
    public static class Initializers
    {
        public static async Task<IApplicationBuilder> InitializerService(this IApplicationBuilder app, bool deleteDatabase = false)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            await DbInitializer(services, deleteDatabase);

            return app;
        }

        public static async Task DbInitializer(IServiceProvider serviceProvider, bool deleteDatabase = false)
        {
            try
            {
                var context = serviceProvider.GetRequiredService<ManagementContext>();
                await ManagementInitializer.InitializeManagementDatabase(context, deleteDatabase);
            }
            catch (Exception)
            {
            }
        }
    }
}