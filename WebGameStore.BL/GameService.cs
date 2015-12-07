using WebGameStore.DAL;
using WebGameStore.Model;

namespace WebGameStore.BL
{
    public class GameService : EntityService<Game>, IGameService
    {
        IUnitOfWork _unitOfWork;
        IGameRepository _gameRepository;

        public GameService(IUnitOfWork unitOfWork, IGameRepository gameRepository)
            : base(unitOfWork, gameRepository)
        {
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
        }
    }
}
