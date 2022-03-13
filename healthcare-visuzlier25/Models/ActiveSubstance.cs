using System;
using System.Collections.Generic;

namespace healthcare_visuzlier25.Models
{
    public partial class ActiveSubstance
    {
        public ActiveSubstance()
        {
            ProductActiveSubstanceMaps = new HashSet<ProductActiveSubstanceMap>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Concentration { get; set; }
        public string ConcUnit { get; set; } = null!;

        public virtual ICollection<ProductActiveSubstanceMap> ProductActiveSubstanceMaps { get; set; }
    }
}
