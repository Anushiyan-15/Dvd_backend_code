using DVDRetal.Entity;
using DVDRetal.Model.RequestModels;
using DVDRetal.Model.ResponseModels;

namespace DVDRetal.IService
{
    public interface ICustomerService
    {

        Task<CustomerResponseModel> AddCustomer(CustomerRequestModel customerRequestModel);

        Task<CustomerResponseModel> GetCustomerById(int id);
        Task<CustomerResponseModel> GetCustomerByNIC(int nic);
        Task<List<CustomerResponseModel>> GetAllCustomers();
        Task<CustomerResponseModel> UpdateCustomer(int id, CustomerRequestModel customerRequestModel);



        Task<CustomerResponseModel> SoftDelete(int id);
    }
}
