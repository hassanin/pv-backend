using System;
using System.Collections.Generic;

namespace healthcare_visuzlier25.Models
{
    public partial class Tenant
    {
        public Tenant()
        {
            DataUsers = new HashSet<DataUser>();
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<DataUser> DataUsers { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
