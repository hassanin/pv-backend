using System;
using System.Collections.Generic;

namespace healthcare_visuzlier25.Models
{
    public partial class Person
    {
        public Person()
        {
            Reports = new HashSet<Report>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Password { get; set; }
        public string Email { get; set; } = null!;
        public string PersonType { get; set; } = null!;

        public virtual ICollection<Report> Reports { get; set; }
    }
}
