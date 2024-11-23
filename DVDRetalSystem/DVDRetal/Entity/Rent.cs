using System.ComponentModel.DataAnnotations;

namespace DVDRetal.Entity
{
    public class Rent
    {
        [Key]
        public Guid RentalId { get; set; }
        public int CustomerID { get; set; }
        public int DVDId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? Returndate { get; set; }
        public bool Isoverdue { get; set; } = false;
        public string? status { get; set; } = "Pending";
        public Customer Customer { get; set; }
        public DVD DVD { get; set; }





    }
}
