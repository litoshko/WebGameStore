using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using WebGameStore.BL;
using WebGameStore.Controllers;
using WebGameStore.Model;

namespace WebGameStore.Tests.ControllersTests
{
    [TestFixture]
    class GameControllerTests
    {
        private Mock<IGameService> _mockView;
        private IGameService _service;
        private GamesController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockView = new Mock<IGameService>();
            _service = _mockView.Object;
            _controller = new GamesController(_service);
        }

        [Test]
        public void GetGames_WhenCalled_CallsServiceGetAllOnce()
        {
            _controller.GetGames();

            _mockView.Verify(m => m.GetAll(), Times.Once);
        }

        [Test]
        public void GetGame_ForWrongId_ReturnsGame()
        {
            // Arrange
            var id = "6";
            _mockView.Setup(m => m.GetById(id)).Returns((Game)null);

            // Act
            var result = _controller.GetGame(id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
            _mockView.Verify(m => m.GetById(id), Times.Once);
        }

        [Test]
        public void GetGame_ForCorrectId_ReturnsGame()
        {
            // Arrange
            var id = "6";
            var game = new Game {Key = id, Name = "Test Name"};
            _mockView.Setup(m => m.GetById(id)).Returns(game);

            // Act
            var result = _controller.GetGame(id);

            // Assert
            _mockView.Verify(m => m.GetById(id), Times.Once);
            Assert.IsInstanceOf<OkNegotiatedContentResult<Game>>(result);
        }

        [Test]
        public void PutGame_ForValidGame_CallsUpdateService()
        {
            // Arrange
            var id = "6";
            var game = new Game { Key = id, Name = "Test Name" };

            // Act
            var result = _controller.PutGame(game);

            // Assert
            _mockView.Verify(m => m.Update(game), Times.Once);
        }

        [Test]
        public void PutGame_ForValidGame_CallsCreateService()
        {
            // Arrange
            var id = "6";
            var game = new Game { Key = id, Name = "Test Name" };

            // Act
            var result = _controller.PostGame(game);

            // Assert
            _mockView.Verify(m => m.Create(game), Times.Once);
        }

        [Test]
        public void DeleteGame_ForValidGame_CallsDeleteService()
        {
            // Arrange
            var id = "6";
            var game = new Game { Key = id, Name = "Test Name" };
            _mockView.Setup(m => m.GetById(id)).Returns(game);

            // Act
            var result = _controller.DeleteGame(game);

            // Assert
            _mockView.Verify(m => m.Delete(game), Times.Once);
        }

        [Test]
        public void DeleteGame_ForValidGame_ReturnsOk()
        {
            // Arrange
            var id = "6";
            var game = new Game { Key = id, Name = "Test Name" };
            _mockView.Setup(m => m.GetById(id)).Returns(game);

            // Act
            var result = _controller.DeleteGame(game);

            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<Game>>(result);
        }

        [Test]
        public void DeleteGame_WrongGame_ReturnsNotFound()
        {
            // Arrange
            var id = "6";
            var game = new Game { Key = id, Name = "Test Name" };
            _mockView.Setup(m => m.GetById(id)).Returns((Game) null);

            // Act
            var result = _controller.DeleteGame(game);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void GetGamesByGenre_ForStringName_CallsGetByGenre()
        {
            // Arrange
            var name = "6";
            _mockView.Setup(m => m.GetByGenre(name)).Returns((IEnumerable<Game>)null);

            // Act
            var result = _controller.GetGamesByGenre(name);

            // Assert
            _mockView.Verify(m => m.GetByGenre(name), Times.Once);
        }

        [Test]
        public void GetGamesByPlatform_ForStringName_CallsGetByPlatform()
        {
            // Arrange
            var name = "6";
            _mockView.Setup(m => m.GetByPlatform(name)).Returns((IEnumerable<Game>)null);

            // Act
            var result = _controller.GetGamesByPlatform(name);

            // Assert
            _mockView.Verify(m => m.GetByPlatform(name), Times.Once);
        }



        [Test]
        public void DownloadGame_ForCorrectId_ReturnsResponse()
        {
            // Arrange
            var id = "6";
            var game = new Game { Key = id, Name = "Test Name" };
            _mockView.Setup(m => m.GetById(id)).Returns(game);

            // Act
            var result = _controller.DownloadGame(id);

            // Assert
            Assert.IsInstanceOf<HttpResponseMessage>(result);
        }
    }
}
