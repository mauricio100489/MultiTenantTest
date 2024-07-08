namespace MultiTenantTest.Infrastructure.Context.Management
{
    public static class ManagementInitializer
    {
        public static async Task InitializeManagementDatabase(ManagementContext context, bool newDatabase = false)
        {
            try
            {
                var databaseExists = await context.Database.CanConnectAsync();

                if (newDatabase && databaseExists)
                {
                    var resultDelete = await context.Database.EnsureDeletedAsync();

                    if (!resultDelete)
                    {
                        throw new Exception("Cannot delete database");
                    }
                }

                if (newDatabase || !databaseExists)
                {
                    var result = await context.Database.EnsureCreatedAsync();

                    if (!result)
                    {
                        throw new Exception("Cannot create database");
                    }

                    await SeedDataAsync(context);
                }
            }
            catch (Exception ex)
            {
                var exception = ex;
            }
        }

        private static async Task SeedDataAsync(ManagementContext context)
        {
            await Task.Delay(1);
            // Si aun caso se desea data de prueba cuando se cree la nueva base de datos.
        }
    }
}
