using MultiTenantTest.Domain.Entities.General;

namespace MultiTenantTest.Domain.Entities.Management
{
    public class Organization : BaseProperties
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SlugTenant { get; set; } = string.Empty;
    }
}
