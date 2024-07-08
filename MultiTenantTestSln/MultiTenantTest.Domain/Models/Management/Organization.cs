using MultiTenantTest.Domain.Models.General;

namespace MultiTenantTest.Domain.Models.Management
{
    public class Organization : BaseProperties
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SlugTenant { get; set; } = string.Empty;
    }
}
