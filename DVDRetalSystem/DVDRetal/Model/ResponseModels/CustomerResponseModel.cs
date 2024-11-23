using DVDRetal.Entity;

namespace DVDRetal.Model.ResponseModels
{
    public class CustomerResponseModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int NIC { get; set; }
        public string Password { get; set; }
        public ICollection<Rent> Rentals { get; set; }
        public bool IsActive { get; set; }



    }
}
