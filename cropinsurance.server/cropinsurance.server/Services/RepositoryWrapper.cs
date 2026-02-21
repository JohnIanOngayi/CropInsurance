using cropinsurance.server.Repository;
using cropinsurance.server.Repository.Contracts;

namespace cropinsurance.server.Services
{
    public class RepositoryWrapper(DbContext dbContext) : IRepositoryWrapper
    {
        private readonly DbContext _dbContext = dbContext;
        private ISeasonRepository? _seasonRepository;
        private ICropRepository? _cropRepository;
        private IApplicationRepository? _insuranceRepository;

        public IApplicationRepository Applications
        {
            get
            {
                _insuranceRepository ??= new ApplicationRepository(_dbContext);
                return _insuranceRepository;
            }
        }


        public ISeasonRepository Seasons
        {
            get
            {
                _seasonRepository ??= new SeasonRepository(_dbContext);
                return _seasonRepository;
            }
        }

        public ICropRepository Crops
        {
            get
            {
                _cropRepository ??= new CropRepository(_dbContext);
                return _cropRepository;
            }
        }
    }
}
