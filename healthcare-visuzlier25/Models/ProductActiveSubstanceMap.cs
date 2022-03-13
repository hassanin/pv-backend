using System;
using System.Collections.Generic;

namespace healthcare_visuzlier25.Models
{
    public partial class ProductActiveSubstanceMap
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? SubstanceId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual ActiveSubstance? Substance { get; set; }
    }
}
