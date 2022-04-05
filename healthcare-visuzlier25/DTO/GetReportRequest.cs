namespace healthcare_visuzlier25.DTO
{
    public class GetReportRequest
    {
        public int? TenantId { get; set; }
        public int Offset { get; set; }
        public int Skip { get; set; }
    }
}
