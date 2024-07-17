using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace spice_delight_app_backend.Models
{
    public class Product
    {
        [Key]
        public int IdMeal { get; set; }
        public string StrMealThumb { get; set; }
        public string StrMeal { get; set; }
        [Precision(18,2)]
        public decimal Price { get; set; }
        public Boolean FastDelivery { get; set; }
        public int Ratings { get; set; }
        public Boolean InStock { get; set; }
        public string StrCategory { get; set; }
    }

}