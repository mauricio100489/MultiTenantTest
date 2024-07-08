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
            await IMCInitializer(services, deleteDatabase);
            return app;
        }

        public static async Task IMCInitializer(IServiceProvider serviceProvider, bool deleteDatabase = false)
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