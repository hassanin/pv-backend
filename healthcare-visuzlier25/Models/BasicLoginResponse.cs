namespace healthcare_visuzlier25.Models
{
    public record BasicLoginResponse
    {
        public string UserName { get; init; }
        public string Email { get; init; }

        public string UserType { get; init; }
        public ICollection<string> Tenants { get; init; } = new List<string>();
    }
}
