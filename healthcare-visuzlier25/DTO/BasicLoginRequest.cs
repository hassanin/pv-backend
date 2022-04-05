namespace healthcare_visuzlier25.Models
{
    public record BasicLoginRequest
    {
        public string Email { get; init; }
        public string Password { get; init; }
    }
}
