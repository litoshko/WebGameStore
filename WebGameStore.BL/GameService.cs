using System.Collections.Generic;
using System.Linq;
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

        public Game GetById(string id)
        {
            return _gameRepository.GetById(id);
        }

        public IEnumerable<Game> GetByGenre(string name)
        {
            // TODO: Fix errors, which are caused by the statement below
            return _gameRepository.FindBy(g => g.GameGenres.Any(gg => gg.GenreName == name));
        }

        public IEnumerable<Game> GetByPlatform(string name)
        {
            // TODO: Fix errors, which are caused by the statement below
            return _gameRepository.FindBy(g => g.GamePlatformTypes.Any(gg => gg.PlatformTypeType == name));
        }
    }
}
