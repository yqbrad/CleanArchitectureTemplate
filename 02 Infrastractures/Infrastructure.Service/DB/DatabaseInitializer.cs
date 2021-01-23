using DDD.Contracts._Base;

namespace DDD.Infrastructure.Service.DB
{
    public class DatabaseInitializer
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkConfiguration _configuration;

        public DatabaseInitializer(IUnitOfWork unitOfWork, IUnitOfWorkConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public void Initialize() => _unitOfWork.InitiateDatabase(_configuration.Seed);
    }
}