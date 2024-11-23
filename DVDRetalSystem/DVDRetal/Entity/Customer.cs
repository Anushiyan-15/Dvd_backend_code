using System.ComponentModel.DataAnnotations;

namespace DVDRetal.Entity
{
    public class Customer
    {
        [Key] 
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Phonenumber {  get; set; }

        public string Email { get; set; }

        public int NIC { get; set; }
        public string Password { get; set; }
        public ICollection<Rent>Rentals { get; set; }


        public bool IsActive { get; set; }=true;
        public Role Role { get; set; }

    }
}
