using cropinsurance.server.Models;

namespace cropinsurance.server.Repository.Contracts
{
    public interface ISeasonRepository
    {
        Task<IEnumerable<Season>> GetAllSeasonsAsync();
        Task<Season?> GetSeasonByIdAsync(int id);
        Task<ResponseModel> CreateSeasonAsync(Season season);
        Task<ResponseModel> UpdateSeasonAsync(Season season);
        Task<ResponseModel> DeleteSeasonAsync(int id);
    }
}
