using WebGameStore.DAL;
using WebGameStore.Model;

namespace WebGameStore.BL
{
    public class GameGenreService : EntityService<GameGenre>, IGameGenreService
    {
        IUnitOfWork _unitOfWork;
        IGameGenreRepository _gameGenreRepository;

        public GameGenreService(IUnitOfWork unitOfWork, IGameGenreRepository gameGenreRepository)
            : base(unitOfWork, gameGenreRepository)
        {
            _unitOfWork = unitOfWork;
            _gameGenreRepository = gameGenreRepository;
        }
    }
}
