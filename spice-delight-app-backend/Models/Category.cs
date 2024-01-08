using System.ComponentModel.DataAnnotations;

namespace spice_delight_app_backend.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Description { get; set; }
    }
}