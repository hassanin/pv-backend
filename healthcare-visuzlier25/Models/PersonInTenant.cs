using System;
using System.Collections.Generic;

namespace healthcare_visuzlier25.Models
{
    public partial class PersonInTenant
    {
        public int? PersonId { get; set; }
        public int? TenantId { get; set; }

        public virtual Person? Person { get; set; }
        public virtual Tenant? Tenant { get; set; }
    }
}
