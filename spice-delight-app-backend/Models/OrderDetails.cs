using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace spice_delight_app_backend.Models
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailID { get; set; }
        public int OrderID { get; set; }
        [ForeignKey("OrderID")]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        public int Quantity { get; set; }

        [Precision(18, 2)]
        public decimal PricePerUnit { get; set; }
    }
}
