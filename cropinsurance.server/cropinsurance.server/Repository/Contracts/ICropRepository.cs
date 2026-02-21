using cropinsurance.server.Models;

namespace cropinsurance.server.Repository.Contracts
{
    public interface ICropRepository
    {
        Task<IEnumerable<Crop>> GetAllCropsAsync();
        Task<IEnumerable<Crop>> GetCropsBySeasonAsync(int seasonId);
        Task<Crop?> GetCropByIdAsync(int id);
        Task<ResponseModel> CreateCropAsync(Crop crop);
        Task<ResponseModel> UpdateCropAsync(Crop crop);
        Task<ResponseModel> DeleteCropAsync(int id);
    }
}
