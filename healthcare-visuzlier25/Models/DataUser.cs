using System;
using System.Collections.Generic;

namespace healthcare_visuzlier25.Models
{
    public partial class DataUser
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Department { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public int TenantId { get; set; }

        public virtual Tenant Tenant { get; set; } = null!;
    }
}
