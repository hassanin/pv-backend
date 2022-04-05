namespace healthcare_visuzlier25.DTO
{
    public class ReportResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public int? TenantId { get; set; }
        public string ReportUrl { get; set; } = null!;
        public string ContainerBase { get; set; } = null!;
        public string SasToken { get; set; } = null!;
        public string FinalUrl { get; set; } = null!;
    }
}
