using spice_delight_app_backend.Models;

namespace spice_delight_app_backend.Data
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Customer> GetCustomers([Service] SpiceDbContext context) =>
            context.Customers;
    }
}
