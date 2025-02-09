﻿using MultiTenantTest.Domain.Entities.General;

namespace MultiTenantTest.Domain.Entities.Management
{
    public class User : BaseProperties
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
        public Organization? Organization { get; set; }
    }
}
