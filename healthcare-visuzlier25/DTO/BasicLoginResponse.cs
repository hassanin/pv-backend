using healthcare_visuzlier25.DTO;

namespace healthcare_visuzlier25.Models
{
    public record BasicLoginResponse
    {
        public string UserName { get; init; }
        public string Email { get; init; }

        public string UserType { get; init; }
        public ICollection<TenantDTO> Tenants { get; init; } = new List<TenantDTO>();
        public string Session { get; init; }
    }
}
