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
        private IGameGenreRepository _gameGenreRepository;
        private IGamePlatformTypeRepository _gamePlatformTypeRepository;

        public GameService(IUnitOfWork unitOfWork, 
                           IGameRepository gameRepository, 
                           IGameGenreRepository gameGenreRepository,
                           IGamePlatformTypeRepository gamePlatformTypeRepository)
            : base(unitOfWork, gameRepository)
        {
            _unitOfWork = unitOfWork;
            _gameRepository = gameRepository;
            _gameGenreRepository = gameGenreRepository;
            _gamePlatformTypeRepository = gamePlatformTypeRepository;
        }

        public Game GetById(string id)
        {
            return _gameRepository.GetById(id);
        }

        public IEnumerable<Game> GetByGenre(string name)
        {
            var ggg = _gameGenreRepository.GetAll().Where(gg => gg.Genre.Name == name);
            return _gameRepository.FindBy(g => ggg.Any(gg => g.Key == gg.GameKey));
        }

        public IEnumerable<Game> GetByPlatform(string name)
        {
            var ggg = _gamePlatformTypeRepository.GetAll().Where(gg => gg.PlatformType.Type == name);
            return _gameRepository.FindBy(g => ggg.Any(gg => g.Key == gg.GameKey));
        }
    }
}
