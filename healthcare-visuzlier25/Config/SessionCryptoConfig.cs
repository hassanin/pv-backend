using System.ComponentModel.DataAnnotations;

namespace healthcare_visuzlier25.Config
{
    public class SessionCryptoConfig
    {
        [Required()]
        public string Key { get; set; }
        [Required()]
        public string IV { get; set; }
    }
}
