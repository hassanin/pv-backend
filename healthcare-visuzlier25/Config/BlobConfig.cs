using System.ComponentModel.DataAnnotations;

namespace healthcare_visuzlier25.Config
{
    public class BlobConfig
    {
        [Required()]
        public string ConnectionString { get; set; } = null!;

        [Required()]
        public string ContainerName { get; set; } = null!;
    }
}
