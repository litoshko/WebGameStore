using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using WebGameStore.BL;
using WebGameStore.Controllers;
using WebGameStore.Model;

namespace WebGameStore.Tests
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
            Mock<IContainer> mockContainer = new Mock<IContainer>();
            _mockView = new Mock<IGameService>();
            _service = _mockView.Object;
            _controller = new GamesController(_service);
        }

        [Test]
        public void GetGame_WithoutParameter_CallsServiceGetAllOnce()
        {
            _controller.GetGames();

            _mockView.Verify(m => m.GetAll(), Times.Once);
        }
    }
}
