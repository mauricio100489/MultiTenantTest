﻿using System.ComponentModel.DataAnnotations;

namespace MultiTenantTest.Domain.Models.General
{
    public class BaseProperties
    {
        [Required]
        public DateTimeOffset CreatedDateTimeOffset { get; set; }
        public DateTimeOffset? LastUpdatedDateTimeOffset { get; set; }
    }
}
