using cropinsurance.server.Models;

namespace cropinsurance.server.Repository.Contracts
{
    public interface IApplicationRepository
    {
        Task<ResponseModel> CreateApplicationAsync(InsuranceApplication insuranceApplication);
        Task<ResponseModel> EditApplicationAsync(InsuranceApplication insuranceApplication);
        Task<IEnumerable<InsuranceApplication>> GetAllApplicationsAsync();
        Task<InsuranceApplication?> GetApplicationAsync(int applicationId);
        Task<ResponseModel> DeleteApplicationAsync(int applicationId);
    }
}
