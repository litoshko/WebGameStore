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
    class CommentServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<ICommentRepository> _commentRepository;
        private List<Comment> _listComment;
        private CommentService _commentService;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _commentRepository = new Mock<ICommentRepository>();
            _listComment = new List<Comment>
            {
                new Comment {Id = 1, Name = "One"},
                new Comment {Id = 2, Name = "Two"},
                new Comment {Id = 3, Name = "Three"}
            };
            _commentService = new CommentService(_unitOfWork.Object,
                                           _commentRepository.Object);
        }

        [Test]
        public void GetCommentsForGame_ForCorrectId_ReturnsComments()
        {
            //Arrange
            var id = "1";
            _commentRepository.Setup(x => x.GetCommentsForGame(It.IsAny<string>())).Returns(_listComment.AsEnumerable());

            //Act
            var result = _commentService.GetCommentsForGame(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Comment>>(result);
            _commentRepository.Verify(m => m.GetCommentsForGame(id), Times.Once());
        }

        [Test]
        public void GetById_ForCorrectId_ReturnsComment()
        {
            //Arrange
            var id = 1;
            _commentRepository.Setup(x => x.GetById(It.IsAny<int>())).Returns(_listComment[0]);

            //Act
            Comment result = _commentService.GetById(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Comment>(result);
            _commentRepository.Verify(m => m.GetById(id), Times.Once());
        }
    }
}
