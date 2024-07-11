using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using spice_delight_app_backend.Data;
using spice_delight_app_backend.Models;

namespace spice_delight_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StripeController: ControllerBase
    {
        private readonly SpiceDbContext _context;

        public StripeController(SpiceDbContext context)
        {
            _context = context;
        }

        [HttpGet("{keyName}")]
        public async Task<ActionResult<Stripe>> GetKey(string keyName)
        {
            var key = await _context.Stripe.FirstOrDefaultAsync(s => s.KeyName == keyName);

            if (key == null)
            {
                return NotFound();
            }

            return key;
        }
    }
}
