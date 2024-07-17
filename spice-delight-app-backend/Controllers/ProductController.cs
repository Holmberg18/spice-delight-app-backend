using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spice_delight_app_backend.Data;
using spice_delight_app_backend.Models;


namespace spice_delight_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly SpiceDbContext _context;

        public ProductController(SpiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpGet("{idMeal}")]
        public async Task<ActionResult<Product>> GetProduct(int idMeal)
        {
            var product = await _context.Products.FindAsync(idMeal);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
    }
}
