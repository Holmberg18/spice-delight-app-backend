using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace spice_delight_app_backend.Models
{
    public class Customer
    {
        [Key] public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }

        public void SetPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                PasswordHash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

    }
}
