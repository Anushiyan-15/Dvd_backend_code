using DVDRetal.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DVDRetal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IRentalService _rentalService;

        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetRentalById(Guid id)
        {
            // Get rental by ID via the service
            var rentalResponse = await _rentalService.GetRentalById(id);

            if (rentalResponse == null)
            {
                return NotFound(new { message = "Rental not found" });
            }

            return Ok(rentalResponse);



        }




    }
}
