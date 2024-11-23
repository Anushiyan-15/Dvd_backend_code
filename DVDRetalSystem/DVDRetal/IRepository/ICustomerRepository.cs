using DVDRetal.Entity;

namespace DVDRetal.IRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> AddCustomer(Customer customer);
        Task<Customer> GetCustomerById(int id);

        Task<Customer> GetCustomerByNIC(int nic);

        Task<List<Customer>> GetAllCustomers();
        Task<Customer> UpdateCustomer(Customer customer);

        Task<Customer> SoftDelete(Customer customer);

    }
}
