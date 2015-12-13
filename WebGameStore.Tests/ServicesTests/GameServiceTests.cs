using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using WebGameStore.BL;
using WebGameStore.DAL;
using WebGameStore.Model;

namespace WebGameStore.Tests.ServicesTests
{
    [TestFixture]
    class GameServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IGameRepository> _gameRepository;
        private Mock<IGameGenreRepository> _gameGenreRepository;
        private Mock<IGamePlatformTypeRepository> _gamePlatformTypeRepository;
        private List<Game> _listGame;
        private List<GameGenre> _listGameGenre;
        private List<GamePlatformType> _listGamePlatform;
        private GameService _gameService;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _gameRepository = new Mock<IGameRepository>();
            _gameGenreRepository = new Mock<IGameGenreRepository>();
            _gamePlatformTypeRepository = new Mock<IGamePlatformTypeRepository>();
            _listGame = new List<Game>
            {
                new Game {Key = "1", Name = "One"},
                new Game {Key = "2", Name = "Two"},
                new Game {Key = "3", Name = "Three"}
            };
            _listGameGenre = new List<GameGenre>
            {
                new GameGenre {Id = 1, GenreName = "One g", GameKey = "1"},
                new GameGenre {Id = 2, GenreName = "Two g", GameKey = "2"},
                new GameGenre {Id = 3, GenreName = "Three g", GameKey = "3"}
            };
            _listGamePlatform = new List<GamePlatformType>
            {
                new GamePlatformType {Id = 1, PlatformTypeType = "One p", GameKey = "1"},
                new GamePlatformType {Id = 2, PlatformTypeType = "Two p", GameKey = "2"},
                new GamePlatformType {Id = 3, PlatformTypeType = "Three p", GameKey = "3"}
            };
            _gameService = new GameService(_unitOfWork.Object,
                                           _gameRepository.Object,
                                           _gameGenreRepository.Object,
                                           _gamePlatformTypeRepository.Object);
        }

        [Test]
        public void GetById_ForCorrectId_ReturnsGame()
        {
            //Arrange
            var id = "1";
            _gameRepository.Setup(x => x.GetById(id)).Returns(_listGame[0]);

            //Act
            Game result = _gameService.GetById(id);

            //Assert
            Assert.IsNotNull(result);
            _gameRepository.Verify(m => m.GetById(id), Times.Once());
        }

        [Test]
        public void GetByGenre_ForCorrectId_ReturnsGame()
        {
            //Arrange
            var key = "1";
            var name = "One g";
            _gameGenreRepository.Setup(m => m.GetAll()).Returns(_listGameGenre.AsQueryable());
            _gameRepository
                .Setup(m => m.FindBy(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(_listGame.AsQueryable().Where(g=>g.Key == key));

            //Act
            IEnumerable<Game> result = _gameService.GetByGenre(name);

            //Assert
            _gameRepository.Verify(m => m.FindBy(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once());
            _gameGenreRepository.Verify(m => m.GetAll(), Times.Once);
        }

        [Test]
        public void GetByPlatform_ForCorrectId_ReturnsGame()
        {
            //Arrange
            var key = "1";
            var name = "One p";
            _gamePlatformTypeRepository.Setup(m => m.GetAll()).Returns(_listGamePlatform.AsQueryable());
            _gameRepository
                .Setup(m => m.FindBy(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns(_listGame.AsQueryable().Where(g => g.Key == key));

            //Act
            IEnumerable<Game> result = _gameService.GetByPlatform(name);

            //Assert
            _gameRepository.Verify(m => m.FindBy(It.IsAny<Expression<Func<Game, bool>>>()), Times.Once());
            _gamePlatformTypeRepository.Verify(m => m.GetAll(), Times.Once);
        }
    }
}
