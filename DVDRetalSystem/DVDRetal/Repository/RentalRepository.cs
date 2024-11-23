using DVDRetal.Entity;
using DVDRetal.IRepository;
using Microsoft.EntityFrameworkCore;
using DVDRetal.DataBase;

namespace DVDRetal.Repository
{
    public class RentalRepository:IRentalRepository
    {
         private readonly DVDContext _dbContext;

        public RentalRepository(DVDContext dbContext)
        {
            _dbContext = dbContext;
        }

    

            // Get rental by ID
            public async Task<Rent> GetRentalByID(Guid id)
            {
                return await _dbContext.Rents.FindAsync(id);
            }

            // Get all rentals for a specific customer
            public async Task<List<Rent>> GetAllRentalsByCustomerId(int customerId)
            {
                return await _dbContext.Rents
                    .Where(r => r.CustomerID == customerId)
                    .ToListAsync();
            }

            // Add a new rental
            public async Task<Rent> AddRental(Rent rental)
            {
            _dbContext.Rents.Add(rental);
                await _dbContext.SaveChangesAsync();
                return rental;
            }

            // Accept rental
            public async Task<Rent> RentalAccept(Rent rental)
            {
                rental.status = "Accepted"; // Update the status to 'Accepted'
            _dbContext.Rents.Update(rental);
                await _dbContext.SaveChangesAsync();
                return rental;
            }

            // Update rental status to return
            public async Task<Rent> UpdateRentToReturn(Rent rental)
            {
                rental.status = "Returned"; // Update the status to 'Returned'
            _dbContext.Rents.Update(rental);
                await _dbContext.SaveChangesAsync();
                return rental;
            }

            // Get all rentals
            public async Task<List<Rent>> GetAllRentals()
            {
                return await _dbContext.Rents.ToListAsync();
            }

            // Reject a rental
            public async Task RejectRental(Guid rentalId)
            {
                var rental = await _dbContext.Rents.FindAsync(rentalId);
                if (rental != null)
                {
                    rental.status = "Rejected";
                _dbContext.Rents.Update(rental);
                    await _dbContext.SaveChangesAsync();
                }
            }

            // Check and update overdue rentals
            public async Task<List<Guid>> CheckAndUpdateOverdueRentals()
            {
                var overdueRentals = await _dbContext.Rents
                    .Where(r => r.Returndate < DateTime.UtcNow && r.status == "Rent")
                    .ToListAsync();

                var overdueIds = new List<Guid>();
                foreach (var rental in overdueRentals)
                {
                    rental.Isoverdue = true;
                    overdueIds.Add(rental.RentalId);
                }

            _dbContext.Rents.UpdateRange(overdueRentals);
                await _dbContext.SaveChangesAsync();
                return overdueIds;
            }
        }

}
