using System.ComponentModel.DataAnnotations;

namespace MultiTenantTest.Domain.Models.Responses
{
    public abstract class InnerError
    {
        public string Code { get; set; }

        [Required]
        public abstract string ErrorType { get; }
    }
}
