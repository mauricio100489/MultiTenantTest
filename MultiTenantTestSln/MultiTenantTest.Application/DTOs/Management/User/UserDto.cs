namespace MultiTenantTest.Application.DTOs.Management.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Organization { get; set; } = string.Empty;
        public int OrganizationId { get; set; }
    }
}
