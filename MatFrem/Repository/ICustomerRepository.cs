using MatFrem.Models.DomainModel;

namespace MatFrem.Repository
{
    public interface ICustomerRepository
    {

        Task<CustomerModel> AddCustomerID(int id); 

    }
}
