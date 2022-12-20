using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Oblig2_Blog.Controllers;
using Oblig2_Blog.Data;
using Oblig2_Blog.Data.Repository.IRepository;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BlogUnitTests
{
    [TestClass]
    public class PostControllerTest
    {
        Mock<IPostRepository> _repository;
        Mock<UserManager<IdentityUser>> mockUserManager;
        Mock<IUnitOfWork> _unitOfWorkMock;
        ApplicationDbContext _context;

        private List<Post> _posts;
        private List<Tag> _tags;
        private PostViewModel _fakePostViewModel;

        [TestInitialize]
        public void SetupContext()
        {
            mockUserManager = MockHelper.MockUserManager<IdentityUser>();
            _repository = new Mock<IPostRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _posts = new List<Post>
            {
                new Post { PostTitle = "Innlegg 1", PostText = "Test 1", PostId = 1, BlogId = 1,Created = new DateTime(2022, 06, 05),
                    OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b"},
                new Post { PostTitle = "Innlegg 2", PostText = "Test 2", PostId = 2, BlogId = 1,
                    Created = new DateTime(2022, 02, 13),
                    OwnerId = "379bb5e6-6292-4e1e-8f84-47fec88eff93"},
                new Post { PostTitle = "Innlegg 3", PostText = "Test 3", PostId = 3, BlogId = 1,
                    Created = new DateTime(2022, 06, 04),
                    OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b"}
            };
            _fakePostViewModel = new PostViewModel()
            {
                BlogId = 1,
                PostId = 1,
                PostTitle = "Tittel",
                PostText = "Beskrivelse",
                Created = new DateTime(2022, 06, 04),
                OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b"
            };

            _tags = new List<Tag>()
            {
                new Tag { TagId = 1, TagName = "tag1" },
                new Tag { TagId = 2, TagName = "tag2" },
                new Tag { TagId = 3, TagName = "tag3" }
            };
        }

        [TestMethod]
        public void IndexReturnsNotNullResult()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Post.GetAll(null, null));
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void IndexReturnsAllPosts()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Post.GetAll(null, null)).Returns(_posts);

            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(Post));
            Assert.IsNotNull(result, "View Result is null");

            var blogs = result.ViewData.Model as List<Post>;
            Assert.AreEqual(3, _posts.Count, "Got wrong number of blogs");
        }

        [TestMethod]
        public void CreateReturnsNotNullResult()
        {
            // Arrange
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            // Act
            var result = (ViewResult)controller.Create(1);

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void SaveIsCalledWhenPostIsCreated()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Save());
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            // Act
            var result = controller.Create(1);

            // Assert
            _repository.VerifyAll();
        }

        [TestMethod]
        public void CreateViewIsReturnedWhenInputIsNotValid()
        {
            // Arrange
            var viewModel = new PostViewModel()
            {
                PostTitle = "",
                PostText = "",
            };
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            // Act
            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(viewModel, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
                controller.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);

            var result = controller.Create(1) as IActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(validationResults.Count > 0);
        }

        [TestMethod]
        public void DetailsCalledWithNoArgumentsReturnsANotFoundResult()
        {
            //Arrange
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            //Act
            var result = controller.Details(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));

        }

        [TestMethod]
        public void DetailsCalledWithWrongPostIdReturnsANotFoundResult()
        {
            //Arrange
            _unitOfWorkMock.Setup(x => x.Post.GetPostViewModel(42)).Returns(_fakePostViewModel);
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null)
            {
                ControllerContext = MockHelper.FakeControllerContext(true)
            };

            //Act
            var result = controller.Details(42);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));

        }

        [TestMethod]
        public void DetailsReturnsAPost()
        {
            //Arrange
            PostViewModel post = new PostViewModel()
            {
                BlogId = 1,
                PostId = 1,
                PostTitle = "Tittel",
                PostText = "Beskrivelse",
                Created = new DateTime(2022, 06, 04),
                OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b",
                Tags = _tags
            };

            _unitOfWorkMock.Setup(x => x.Post.GetPostViewModel(1)).Returns(post);
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            //Act
            var result = (ViewResult)controller.Details(1);
            var postReturn = result.ViewData.Model as PostViewModel;

            //Assert
            Assert.AreEqual(post, postReturn, "Got wrong post back");
        }

        [TestMethod]
        public void CreateReturnsAView()
        {
            //Arrange
            Post post = new Post { PostTitle = "Hallo", PostText = "TEXT TEST", PostId = 1 };
            _repository.Setup(x => x.GetFirstOrDefault(y => y.PostId == 1)).Returns(post);
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            //Act
            ActionResult result = controller.Create(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void EditCalledWithWrongPostIdReturnsANotFoundResult()
        {

            //Arrange
            Post post = new Post { PostTitle = "Hallo", PostText = "TEXT TEST", PostId = 1 };
            _unitOfWorkMock.Setup(x => x.Post.GetFirstOrDefault(y => y.PostId == 1)).Returns(post);
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            //Act
            var result = controller.Edit(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void EditCalledWithNoArgumentsReturnsANotFoundResult()
        {

            //Arrange
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            //Act
            var result = controller.Edit(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void CreateReturnsInstanceOfPostEditViewModel()
        {
            // Arrange

            _unitOfWorkMock.Setup(x => x.Post.GetPostViewModel(null)).Returns(_fakePostViewModel);
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null)
            {
                ControllerContext = MockHelper.FakeControllerContext(true)
            };
            // Act
            var result = (ViewResult)controller.Create(1);
            // Assert
            Assert.IsNotNull(result, "Result is null");
            Assert.AreSame(_fakePostViewModel, result.Model);
        }

        [TestMethod]
        public void DeleteCalledWithNoArgumentsReturnsANotFoundResult()
        {
            //Arrange
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            //Act
            var result = controller.Delete(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void DeleteCalledWithWrongPostIdReturnsANotFoundResult()
        {
            //Arrange
            _unitOfWorkMock.Setup(x => x.Post.GetFirstOrDefault(y => y.PostId == 2)).Returns<Post>(null);
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            //Act
            var result = controller.Delete(42);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void DeleteConfirmedCallsDeleteInIRepository()
        {
            //Arrange
            Post post = new Post { PostTitle = "Hallo", PostText = "TEXT TEST", PostId = 1, BlogId = 1 };
            _unitOfWorkMock.Setup(x => x.Post.GetFirstOrDefault(y => y.PostId == 1)).Returns(post);
            var controller = new PostController(_unitOfWorkMock.Object, mockUserManager.Object, null);

            //Act
            controller.DeleteConfirmed(1, 1);

            // Assert
            _unitOfWorkMock.Verify(x => x.Post.Remove(post));

        }
    }
}
