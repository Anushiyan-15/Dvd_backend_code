using DVDRetal.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DVDRetal.Entity;
using DVDRetal.Model.RequestModels;

namespace DVDRetal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {


            _customerService = customerService;
        }

        [HttpPost("AddCustomer")]

        public async Task<IActionResult> AddCustomer(CustomerRequestModel customer)
        {
            if (customer == null)
            {

                return BadRequest(" Customerdataisrequired");
            }

            var customers = await _customerService.AddCustomer(customer);


            return Ok(customers);


        }

        [HttpGet("Get Customer ById{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var result = await _customerService.GetCustomerById(id);
                if (result == null)
                    return NotFound("Customer not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("Get Customer By NIC{nic}")]
        public async Task<IActionResult> GetCustomerByNIC(int nic)
        {
            try
            {
                var result = await _customerService.GetCustomerByNIC(nic);
                if (result == null)
                    return NotFound("Customer not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("Get Customers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var result = await _customerService.GetAllCustomers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




        [HttpPut(" Update Customers")]
        public async Task<IActionResult> UpdateCustomer( int id, CustomerRequestModel customerRequestModel)
        {
            try
            {
                var result = await _customerService.UpdateCustomer(id, customerRequestModel);
                if (result == null)
                    return NotFound("Customer not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDeleteCustomer(int id)
        {
            var result = await _customerService.SoftDelete(id);

            if (result == null)
            {
                return NotFound(new { message = "Customer not found" });
            }

            return Ok(result); // Returns the soft-deleted customer details
        }








    }
}
