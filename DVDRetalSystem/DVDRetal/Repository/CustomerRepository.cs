using DVDRetal.DataBase;
using DVDRetal.Entity;
using DVDRetal.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DVDRetal.Repository
{
    public class CustomerRepository:ICustomerRepository
    {
      private readonly DVDContext _context;

        public CustomerRepository(DVDContext context) {
        
         _context = context;
        
        }

        public async Task<Customer>AddCustomer(Customer customer)
        {

          var customers= await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
            return customers.Entity;


        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);


        }

       public async Task<Customer> GetCustomerByNIC(int nic)
        {
            return await _context.Customers.FirstOrDefaultAsync(c => c.NIC == nic);

        }


          public async Task<List<Customer>> GetAllCustomers()
        {

            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> UpdateCustomer(Customer customer)
        {
            var existingCustomer = await _context.Customers.FindAsync(customer.Id);
            if (existingCustomer == null)
            {
                return null;
            }
            existingCustomer.UserName = customer.UserName;
            existingCustomer.Phonenumber = customer.Phonenumber;
            existingCustomer.Email = customer.Email;
            existingCustomer.NIC = customer.NIC;
            existingCustomer.Password = customer.Password; 

          
            await _context.SaveChangesAsync();

            return existingCustomer;
        }

        public async Task<Customer> SoftDelete(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));

            // Mark the customer as deleted (soft delete)
            customer.IsActive = false;
          

            // Save the changes to the database
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();

            return customer;
        }









    }
}
