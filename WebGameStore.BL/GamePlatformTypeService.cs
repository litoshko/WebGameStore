using WebGameStore.DAL;
using WebGameStore.Model;

namespace WebGameStore.BL
{
    public class GamePlatformTypeService : EntityService<GamePlatformType>, IGamePlatformTypeService
    {
        IUnitOfWork _unitOfWork;
        IGamePlatformTypeRepository _gamePlatformTypeRepository;

        public GamePlatformTypeService(IUnitOfWork unitOfWork, IGamePlatformTypeRepository gamePlatformTypeRepository)
            : base(unitOfWork, gamePlatformTypeRepository)
        {
            _unitOfWork = unitOfWork;
            _gamePlatformTypeRepository = gamePlatformTypeRepository;
        }
    }
}
