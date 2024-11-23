using DVDRetal.Entity;

namespace DVDRetal.IRepository
{
    public interface IRentalRepository
    {
        
            Task<Rent> GetRentalByID(Guid id);
            Task<List<Rent>> GetAllRentalsByCustomerId(int customerId);
            Task<Rent> AddRental(Rent rental);
            Task<Rent> RentalAccept(Rent rental);
            Task<Rent> UpdateRentToReturn(Rent rental);
            Task<List<Rent>> GetAllRentals();
            Task RejectRental(Guid rentalId);
            Task<List<Guid>> CheckAndUpdateOverdueRentals();
        
    }

    
}
