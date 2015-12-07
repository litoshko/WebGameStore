using WebGameStore.DAL;
using WebGameStore.Model;

namespace WebGameStore.BL
{
    public class PlatformTypeService : EntityService<PlatformType>, IPlatformTypeService
    {
        IUnitOfWork _unitOfWork;
        IPlatformTypeRepository _platformTypeRepository;

        public PlatformTypeService(IUnitOfWork unitOfWork, IPlatformTypeRepository platformTypeRepository)
            : base(unitOfWork, platformTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _platformTypeRepository = platformTypeRepository;
        }
    }
}
