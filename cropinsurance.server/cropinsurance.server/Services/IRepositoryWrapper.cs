using cropinsurance.server.Repository.Contracts;

namespace cropinsurance.server.Services
{
    public interface IRepositoryWrapper
    {
        IApplicationRepository Applications { get; }
        ISeasonRepository Seasons { get; }
        ICropRepository Crops { get; }
    }
}
