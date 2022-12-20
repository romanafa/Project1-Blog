using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Oblig2_Blog.Controllers;
using Oblig2_Blog.Data.Repository.IRepository;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Oblig2_Blog.Data;

namespace BlogUnitTests
{
    [TestClass]
    public class TagUnitTest
    {
        Mock<ITagRepository> _repository;
        Mock<UserManager<IdentityUser>> mockUserManager;
        Mock<IUnitOfWork> _unitOfWorkMock;

        private List<Tag> _tags;
        private TagViewModel _fakeTagViewModel;

        [TestInitialize]
        public void SetupContext()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _repository = new Mock<ITagRepository>();

            _tags = new List<Tag>()
            {
                new Tag { TagId = 1, TagName = "tag1" },
                new Tag { TagId = 2, TagName = "tag2" },
                new Tag { TagId = 3, TagName = "tag3" }
            };

            _fakeTagViewModel = new TagViewModel()
            {
                TagId = 1,
                TagName = "tag1"
            };
        }

        [TestMethod]
        public void IndexReturnsNotNullResult()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Tag.GetAll(null, null));
            var controller = new TagController(_unitOfWorkMock.Object, null);

            // Act
            var result = (ViewResult) controller.Index();

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void IndexReturnsAllTags()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Tag.GetAll(null, null)).Returns(_tags);
            var controller = new TagController(_unitOfWorkMock.Object, null);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Tag));
            Assert.IsNotNull(result, "View Result is null");
            var tags = result.ViewData.Model as List<Tag>;
            Assert.AreEqual(3, _tags.Count, "Got wrong number of blogs");
        }

        [TestMethod]
        public void CreateReturnsNotNullResult()
        {
            // Arrange
            var controller = new TagController(_unitOfWorkMock.Object, null);

            // Act
            var result = (ViewResult)controller.Create();

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void SaveIsCalledWhenTagIsCreated()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Save());
            var controller = new TagController(_unitOfWorkMock.Object, null);

            // Act
            var result = controller.Create(new TagViewModel());

            // Assert
            _repository.VerifyAll();
        }

        [TestMethod]
        public void CreateReturnsAView()
        {
            //Arrange
            Tag tag = new Tag { TagId = 1, TagName = "tag1" };
            _repository.Setup(x => x.GetFirstOrDefault(y => y.TagId == 1)).Returns(tag);
            var controller = new TagController(_unitOfWorkMock.Object, null);

            //Act
            ActionResult result = controller.Create();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void DeleteCalledWithNoArgumentsReturnsANotFoundResult()
        {
            //Arrange
            var controller = new TagController(_unitOfWorkMock.Object,  null);

            //Act
            var Result = controller.Delete(null);

            // Assert
            Assert.IsInstanceOfType(Result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void DeleteCalledWithWrongPostIdReturnsANotFoundResult()
        {
            //Arrange
            _unitOfWorkMock.Setup(x => x.Tag.GetFirstOrDefault(y => y.TagId == 2)).Returns<Tag>(null);
            var controller = new TagController(_unitOfWorkMock.Object, null);

            //Act
            var Result = controller.Delete(42);

            // Assert
            Assert.IsInstanceOfType(Result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void DeleteConfirmedCallsDeleteInIRepository()
        {
            //Arrange
            Tag tag = new Tag { TagId = 1, TagName ="tag1" };
            _unitOfWorkMock.Setup(x => x.Tag.GetFirstOrDefault(y => y.TagId == 1)).Returns(tag);
            var controller = new TagController(_unitOfWorkMock.Object,  null);

            //Act
            controller.DeleteConfirmed(1);

            // Assert
            _unitOfWorkMock.Verify(x => x.Tag.Remove(tag));

        }
    }
}
