using MatFrem.DataContext;
using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDBContext _context;
        public CustomerRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<CustomerModel> AddCustomerID(int id)
        {
            CustomerModel customer = new CustomerModel
            {
                CustomerID = id,
            };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

    }
}
