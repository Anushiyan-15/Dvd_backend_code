using DVDRetal.IRepository;
using DVDRetal.IService;
using DVDRetal.Model.ResponseModels;

using DVDRetal.Entity;
using System.Reflection.Metadata.Ecma335;
using DVDRetal.Model.RequestModels;

namespace DVDRetal.Service
{
    public class CustomerService:ICustomerService
    {

        private ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {


            _customerRepository = customerRepository;
        }

        public async Task<CustomerResponseModel> AddCustomer(CustomerRequestModel customerRequestModel)
        {
            // Check if the UserName or Nic already exists
           
            var customer = new Customer
            {
                UserName = customerRequestModel.UserName,
                Email = customerRequestModel.Email,
                NIC = customerRequestModel.NIC,
                Phonenumber = customerRequestModel.PhoneNumber,
                Password = customerRequestModel.Password
            };

            var createdCustomer = await _customerRepository.AddCustomer(customer);

            return new CustomerResponseModel
            {
                Id = createdCustomer.Id,
                UserName = createdCustomer.UserName,
                Email = createdCustomer.Email,
                NIC = createdCustomer.NIC,
                PhoneNumber = createdCustomer.Phonenumber,
                Password = createdCustomer.Password
            };
        }


        public async Task<CustomerResponseModel> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);

            if (customer == null)
            {
                return null; 
            }

            var customerresponse= new CustomerResponseModel
            {
                Id = customer.Id,
                UserName = customer.UserName,
                PhoneNumber = customer.Phonenumber,
                Password = customer.Password,
                Email = customer.Email,
                IsActive = customer.IsActive,
                Rentals = customer.Rentals
            };

            return customerresponse;
        }

        public async Task<CustomerResponseModel>GetCustomerByNIC(int nic)
        {
            var customer = await _customerRepository.GetCustomerByNIC(nic);

            if(customer == null)
            {
                return null;

            }


            var customerresponse = new CustomerResponseModel
            {

             Id=customer.Id,
             UserName = customer.UserName,
             PhoneNumber = customer.Phonenumber,
             Password = customer.Password,
             Email = customer.Email,
             IsActive = customer.IsActive,
             Rentals = customer.Rentals



            };

            return customerresponse;



        }


        public async Task<List<CustomerResponseModel>> GetAllCustomers()
        {
            var customerlist = await _customerRepository.GetAllCustomers();

            var responselist=new List<CustomerResponseModel>();
            foreach (var customer in customerlist)
            {
                responselist.Add(new CustomerResponseModel
                {Id=customer.Id,
                 UserName = customer.UserName,
                 PhoneNumber = customer.Phonenumber,
                 NIC=customer.NIC,
                 Password = customer.Password,
                 Email = customer.Email,
                 IsActive = customer.IsActive,
                 Rentals = customer.Rentals
                });





            }

            return responselist;
        }

        public async Task<CustomerResponseModel> UpdateCustomer(int id, CustomerRequestModel customerRequestModel)
        {
            // Retrieve the customer from the repository
            var customerToUpdate = await _customerRepository.GetCustomerById(id);
            if (customerToUpdate == null)
            {
                // Return null if customer is not found
                return null;
            }

            // Map the properties from customerRequestModel to customer entity
            customerToUpdate.UserName = customerRequestModel.UserName;
            customerToUpdate.Phonenumber = customerRequestModel.PhoneNumber;
            customerToUpdate.Email = customerRequestModel.Email;
            customerToUpdate.NIC = customerRequestModel.NIC;
            customerToUpdate.Password = customerRequestModel.Password;  // Consider hashing the password

            // Update customer in the repository
            var updatedCustomer = await _customerRepository.UpdateCustomer(customerToUpdate);

            // Return a response model with the updated customer details
            return new CustomerResponseModel
            {
                Id = updatedCustomer.Id,
                UserName = updatedCustomer.UserName,
                PhoneNumber = updatedCustomer.Phonenumber,
                Email = updatedCustomer.Email,
                NIC = updatedCustomer.NIC,
                Password = updatedCustomer.Password, // Again, consider security implications here
                Rentals = updatedCustomer.Rentals
            };
        }

        public async Task<CustomerResponseModel> SoftDelete(int id)
        {
            var customerData = await _customerRepository.GetCustomerById(id);
            var deletedCustomer = await _customerRepository.SoftDelete(customerData);

            return new CustomerResponseModel
            {
                Id = id,
                UserName = deletedCustomer.UserName,
                PhoneNumber = deletedCustomer.Phonenumber,
                Email = deletedCustomer.Email,
                NIC = deletedCustomer.NIC,
                Password = deletedCustomer.Password,
                Rentals = deletedCustomer.Rentals
            };
        }















    }
}
