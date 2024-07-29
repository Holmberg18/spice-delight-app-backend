using spice_delight_app_backend.Models;

namespace spice_delight_app_backend.Helpers
{
    public class CustomerHelper
    {
        public static CustomerResponse CreateResponse(Customer customer)
        {
            var response = new CustomerResponse
            {
                CustomerId = customer.CustomerID,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Username = customer.Username
            };

            return response;
        }
    }
}
