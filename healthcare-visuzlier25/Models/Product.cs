using System;
using System.Collections.Generic;

namespace healthcare_visuzlier25.Models
{
    public partial class Product
    {
        public Product()
        {
            ProductActiveSubstanceMaps = new HashSet<ProductActiveSubstanceMap>();
        }

        public Guid Id { get; set; }
        public string Country { get; set; } = null!;
        public string PharmacuticalForm { get; set; } = null!;
        public string RouteOfAdminstration { get; set; } = null!;
        public string? ProductLifetime { get; set; }
        public string? AuthorizationNumber { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? ReRegistrationDate { get; set; }
        public string? HolderName { get; set; }

        public virtual ICollection<ProductActiveSubstanceMap> ProductActiveSubstanceMaps { get; set; }
    }
}
