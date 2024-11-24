using DVDRetal.Entity;

namespace DVDRetal.IRepository
{
    public interface IManagerRepository
    {
        Task<DVD> AddDVDAsync(DVD dvd);
        Task<DVD> GetDVDByIdAsync(int id);
        Task<List<DVD>> GetAllDVDsAsync(); // Corrected missing method
        Task<DVD> UpdateDVDAsync(DVD dvd);
        Task<DVD> DeleteDVDAsync(int id);
    }
}
