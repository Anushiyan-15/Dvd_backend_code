using DVDRetal.Entity;
using DVDRetal.IRepository;
using DVDRetal.IService;
using DVDRetal.Model.RequestModels;
using DVDRetal.Model.ResponseModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DVDRetal.Service
{
    public class ManagerService:IManagerService
    {
        private readonly IManagerRepository _managerRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ManagerService(IManagerRepository managerRepository, IWebHostEnvironment webHostEnvironment)
        {
            _managerRepository = managerRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        // Add a new DVD with image upload
        public async Task<ManagerResponseModel> AddDVDAsync(ManagerRequestModel managerRequestModel)
        {
            var dvd = new DVD
            {
                Title = managerRequestModel.Title,
                Genre = managerRequestModel.Genre,
                Director = managerRequestModel.Director,
                ReleaseDate = managerRequestModel.ReleaseDate,
                CopiesAvailable = managerRequestModel.CopiesAvailable,
            };

            // Handle image upload
            if (managerRequestModel.ImageFile != null && managerRequestModel.ImageFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(managerRequestModel.ImageFile.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "dvdimages", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath)); // Ensure the directory exists
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await managerRequestModel.ImageFile.CopyToAsync(stream);
                }

                dvd.ImagePath = "/dvdimages/" + fileName;
            }

            var result = await _managerRepository.AddDVDAsync(dvd);

            return new ManagerResponseModel
            {
                Id = result.Id,
                Title = result.Title,
                Genre = result.Genre,
                Director = result.Director,
                ReleaseDate = result.ReleaseDate,
                CopiesAvailable = result.CopiesAvailable,
                ImageUrl = result.ImagePath
            };
        }

        // Get DVD by Id
        public async Task<ManagerResponseModel> GetDVDByIdAsync(int id)
        {
            var dvd = await _managerRepository.GetDVDByIdAsync(id);
            if (dvd == null) return null;

            return new ManagerResponseModel
            {
                Id = dvd.Id,
                Title = dvd.Title,
                Genre = dvd.Genre,
                Director = dvd.Director,
                ReleaseDate = dvd.ReleaseDate,
                CopiesAvailable = dvd.CopiesAvailable,
                ImageUrl = dvd.ImagePath
            };
        }

        // Get all DVDs
        public async Task<List<ManagerResponseModel>> GetAllDVDsAsync()
        {
            var dvds = await _managerRepository.GetAllDVDsAsync();
            var response = new List<ManagerResponseModel>();

            foreach (var dvd in dvds)
            {
                response.Add(new ManagerResponseModel
                {
                    Id = dvd.Id,
                    Title = dvd.Title,
                    Genre = dvd.Genre,
                    Director = dvd.Director,
                    ReleaseDate = dvd.ReleaseDate,
                    CopiesAvailable = dvd.CopiesAvailable,
                    ImageUrl = dvd.ImagePath
                });
            }

            return response;
        }

        // Update DVD with partial updates
        public async Task<ManagerResponseModel> UpdateDVDAsync(int id, ManagerRequestModel managerRequestModel)
        {
            var existingDVD = await _managerRepository.GetDVDByIdAsync(id);
            if (existingDVD == null) return null;

            // Update only the provided fields
            if (!string.IsNullOrEmpty(managerRequestModel.Title))
                existingDVD.Title = managerRequestModel.Title;

            if (!string.IsNullOrEmpty(managerRequestModel.Genre))
                existingDVD.Genre = managerRequestModel.Genre;

            if (!string.IsNullOrEmpty(managerRequestModel.Director))
                existingDVD.Director = managerRequestModel.Director;

            // Handle image replacement
            if (managerRequestModel.ImageFile != null && managerRequestModel.ImageFile.Length > 0)
            {
                // Delete old image if exists
                if (!string.IsNullOrEmpty(existingDVD.ImagePath))
                {
                    var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, existingDVD.ImagePath.TrimStart('/'));
                    if (File.Exists(oldFilePath)) File.Delete(oldFilePath);
                }

                // Save new image
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(managerRequestModel.ImageFile.FileName);
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "dvdimages", fileName);

                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await managerRequestModel.ImageFile.CopyToAsync(stream);
                }

                existingDVD.ImagePath = "/dvdimages/" + fileName;
            }

            var result = await _managerRepository.UpdateDVDAsync(existingDVD);

            return new ManagerResponseModel
            {
                Id = result.Id,
                Title = result.Title,
                Genre = result.Genre,
                Director = result.Director,
                ReleaseDate = result.ReleaseDate,
                CopiesAvailable = result.CopiesAvailable,
                ImageUrl = result.ImagePath
            };
        }

        // Delete DVD
        public async Task<ManagerResponseModel> DeleteDVDAsync(int id)
        {
            var dvd = await _managerRepository.DeleteDVDAsync(id);
            if (dvd == null) return null;

            return new ManagerResponseModel
            {
                Id = dvd.Id,
                Title = dvd.Title,
                Genre = dvd.Genre,
                Director = dvd.Director,
                ReleaseDate = dvd.ReleaseDate,
                CopiesAvailable = dvd.CopiesAvailable,
                ImageUrl = dvd.ImagePath
            };
        }
    }
}
