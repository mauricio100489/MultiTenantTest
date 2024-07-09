using System.ComponentModel.DataAnnotations;

namespace MultiTenantTest.Domain.Entities.Products.Reader
{
    public class ProductR
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(maximumLength: 100, MinimumLength = 10)]
        public string Name { get; set; } = string.Empty;


        [Required]
        [StringLength(maximumLength: 250, MinimumLength = 20)]
        public string Description { get; set; } = string.Empty;
    }
}
