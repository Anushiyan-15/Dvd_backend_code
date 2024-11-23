namespace DVDRetal.Model.ResponseModels
{
    public class RentalResponseModel
    {


        public Guid RentalId { get; set; }
        public int CustomerID { get; set; }
        public int DVDId { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? Returndate { get; set; }
        public bool Isoverdue { get; set; } = false;
        public string status { get; set; } = "Pending";
    }
}
