using DVDRetal.Model.ResponseModels;

namespace DVDRetal.IService
{
    public interface IRentalService
    {

        Task<RentalResponseModel> GetRentalById(Guid id);
    }
}
