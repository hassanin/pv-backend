namespace healthcare_visuzlier25.DTO
{
    public class CreateReportRequest
    {
        public string ReportName { get; init; }
        /// <summary>
        /// Base 64 encoded request 
        /// </summary>
        public string ReportContent { get; init; }
        public int TenantID { get; init; }
    }
}
