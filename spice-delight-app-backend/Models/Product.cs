using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace spice_delight_app_backend.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]

        [Precision(18,2)]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public int SupplierID { get; set; }
        public int StockQuantity { get; set; }
        public int ShelfLife { get; set; }
    }

}