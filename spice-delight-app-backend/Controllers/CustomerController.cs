using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using spice_delight_app_backend.Data;
using spice_delight_app_backend.Models;
using spice_delight_app_backend.DTOs;
using System.Text;

namespace spice_delight_app_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly SpiceDbContext _context;

        public CustomerController(SpiceDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if(customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto)
        {
            if (!CustomerExists(id))
            {
                return BadRequest();
            }

            var customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return NotFound();
            }

            customer.FirstName = customerUpdateDto.FirstName;
            customer.LastName = customerUpdateDto.LastName;
            customer.Email = customerUpdateDto.Email;
            customer.Phone = customerUpdateDto.Phone;
            customer.Address = customerUpdateDto.Address;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            customer.SetPassword(customer.PasswordHash);

            _context.Customers.Add(customer);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerID }, new { customer.CustomerID, customer.FirstName, customer.LastName, customer.Email, customer.Phone, customer.Address, customer.Username });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerID == id);
        }

        // Post: api/Customer/Login
        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Username == login.Username);

            if(customer == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            //Verify the password - TODO - switch to proper password verification
            using (var sha256 = SHA256.Create())
            {
                var hashedPassword = BitConverter.ToString(sha256.ComputeHash(Encoding.UTF8.GetBytes(login.Password))).Replace("-", "").ToLower();
                if(customer.PasswordHash != hashedPassword)
                {
                    return Unauthorized("Invalid password.");
                }
            }

            return Ok(customer);
        }
    }
}