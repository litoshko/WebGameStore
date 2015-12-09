using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Moq;
using NUnit.Framework;
using WebGameStore.BL;
using WebGameStore.Controllers;
using WebGameStore.Model;

namespace WebGameStore.Tests
{
    [TestFixture]
    class CommentControllerTests
    {
        private Mock<IGameService> _mockGameService;
        private Mock<ICommentService> _mockCommentService;
        private CommentsController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockGameService = new Mock<IGameService>();
            _mockCommentService = new Mock<ICommentService>();
            _controller = new CommentsController(_mockCommentService.Object,
                                                 _mockGameService.Object);
        }

        [Test]
        public void GetCommentsForGame_CorrectId_CallsService()
        {
            // Arrange
            var id = "1";

            // Act
            _controller.GetCommentsForGame(id);

            // Assert
            _mockCommentService.Verify(m => m.GetCommentsForGame(id), Times.Once);
        }

        [Test]
        public void AddCommentForGame_ExistingGame_CallsCreateService()
        {
            // Arrange
            var id = "1";
            var comment = new Comment {Name = "Author", Body = "Some body"};
            _mockGameService.Setup(m => m.GetById(id)).Returns(new Game());

            // Act
            _controller.AddCommentForGame(id, comment);

            // Assert
            _mockCommentService.Verify(m => m.Create(comment), Times.Once);
        }

        [Test]
        public void AddCommentForGame_WrongGame_ReturnsNotFound()
        {
            // Arrange
            var id = "1";
            var comment = new Comment { Name = "Author", Body = "Some body" };
            _mockGameService.Setup(m => m.GetById(id)).Returns((Game)null);

            // Act
            var result = _controller.AddCommentForGame(id, comment);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void AddCommentForComment_ExistingComment_CallsCreateService()
        {
            // Arrange
            var id = 1;
            var comment = new Comment { Name = "Author", Body = "Some body" };
            _mockCommentService.Setup(m => m.GetById(id)).Returns(new Comment());

            // Act
            _controller.AddCommentForComment(id, comment);

            // Assert
            _mockCommentService.Verify(m => m.Create(comment), Times.Once);
        }

        [Test]
        public void AddCommentForComment_WrongComment_ReturnsNotFound()
        {
            // Arrange
            var id = 1;
            var comment = new Comment { Name = "Author", Body = "Some body" };
            _mockCommentService.Setup(m => m.GetById(id)).Returns((Comment)null);

            // Act
            var result = _controller.AddCommentForComment(id, comment);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void GetComment_ExistingComment_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var comment = new Comment { Name = "Author", Body = "Some body" };
            _mockCommentService.Setup(m => m.GetById(id)).Returns(new Comment());

            // Act
            var result = _controller.GetComment(id);

            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<Comment>>(result);
        }

        [Test]
        public void GetComment_WrongComment_ReturnsNotFound()
        {
            // Arrange
            var id = 1;
            var comment = new Comment { Name = "Author", Body = "Some body" };
            _mockCommentService.Setup(m => m.GetById(id)).Returns((Comment)null);

            // Act
            var result = _controller.GetComment(id);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }
    }
}
