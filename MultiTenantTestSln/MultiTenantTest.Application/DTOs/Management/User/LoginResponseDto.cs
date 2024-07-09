using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantTest.Application.DTOs.Management.User
{
    public class LoginResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public List<Tenants> Tenants { get; set; }
    }

    public class Tenants
    {
        public string SlugTenant { get; set; }
    }
}
