using DVDRetal.Model.RequestModels;
using DVDRetal.Model.ResponseModels;

namespace DVDRetal.IService
{
    public interface IManagerService
    {
        Task<ManagerResponseModel> AddDVDAsync(ManagerRequestModel managerRequestModel);
        Task<ManagerResponseModel> GetDVDByIdAsync(int id);
        Task<List<ManagerResponseModel>> GetAllDVDsAsync();
        Task<ManagerResponseModel> UpdateDVDAsync(int id, ManagerRequestModel managerRequestModel);
        Task<ManagerResponseModel> DeleteDVDAsync(int id);
    }
}
