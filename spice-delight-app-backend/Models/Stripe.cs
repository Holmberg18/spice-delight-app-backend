using System.ComponentModel.DataAnnotations;

namespace spice_delight_app_backend.Models
{
    public class Stripe
    {
        [Key]
        public int KeyID { get; set; }
        public string KeyName { get; set; }
        public string PublishableKey { get; set; }
        public string SecretKey { get; set; }
    }

}
