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
    [TestFixture(typeof(PlatformType))]
    [TestFixture(typeof(Genre))]
    [TestFixture(typeof(GameGenre))]
    [TestFixture(typeof(GamePlatformType))]
    public class EntityServiceTests<TEntity> 
        where TEntity : class, new()
    {
        private class Service : EntityService<TEntity>
        {
            public Service(IUnitOfWork unitOfWork, IGenericRepository<TEntity> repository)
                : base(unitOfWork, repository)
            {
            }
        }

        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IGenericRepository<TEntity>> _repository;
        private IEntityService<TEntity> _entityService;

        [SetUp]
        public void SetUp()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _repository = new Mock<IGenericRepository<TEntity>>();
            _entityService = new Service(_unitOfWork.Object,
                                           _repository.Object);
        }

        [Test]
        public void Create_ForCorrectEntity_AddAndCommit()
        {
            //Arrange
            TEntity entity = new TEntity();

            //Act
            _entityService.Create(entity);

            //Assert
            _repository.Verify(m => m.Add(entity), Times.Once());
            _unitOfWork.Verify(m => m.Commit(), Times.Once());
        }

        [Test]
        public void Create_ForNullEntity_Exception()
        {
            //Arrange
            TEntity entity = null;

            //Act
            
            //Assert
            Assert.Throws<ArgumentNullException>(delegate { _entityService.Create(entity); });
        }

        [Test]
        public void Update_ForCorrectEntity_EditAndCommit()
        {
            //Arrange
            TEntity entity = new TEntity();

            //Act
            _entityService.Update(entity);

            //Assert
            _repository.Verify(m => m.Edit(entity), Times.Once());
            _unitOfWork.Verify(m => m.Commit(), Times.Once());
        }

        [Test]
        public void Update_ForNullEntity_Exception()
        {
            //Arrange
            TEntity entity = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(delegate { _entityService.Update(entity); });
        }

        [Test]
        public void Delete_ForCorrectEntity_DeleteAndCommit()
        {
            //Arrange
            TEntity entity = new TEntity();

            //Act
            _entityService.Delete(entity);

            //Assert
            _repository.Verify(m => m.Delete(entity), Times.Once());
            _unitOfWork.Verify(m => m.Commit(), Times.Once());
        }

        [Test]
        public void Delete_ForNullEntity_Exception()
        {
            //Arrange
            TEntity entity = null;

            //Act

            //Assert
            Assert.Throws<ArgumentNullException>(delegate { _entityService.Delete(entity); });
        }

        [Test]
        public void GetAll_Always_CallsGetAll()
        {
            //Arrange

            //Act
            _entityService.GetAll();

            //Assert
            _repository.Verify(m => m.GetAll(), Times.Once());
        }
    }
}
