using System;
using System.Collections.Generic;

namespace healthcare_visuzlier25.Models
{
    public partial class Report
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? CreatedBy { get; set; }
        public int? TenantId { get; set; }
        public string ReportUrl { get; set; } = null!;

        public virtual Person? CreatedByNavigation { get; set; }
        public virtual Tenant? Tenant { get; set; }
    }
}
