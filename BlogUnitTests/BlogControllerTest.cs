using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Oblig2_Blog.Controllers;
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

namespace BlogUnitTests
{
    [TestClass]
    public class BlogControllerTest
    {
        Mock<IBlogRepository> _repository;
        Mock<UserManager<IdentityUser>> mockUserManager;
        Mock<IUnitOfWork> _unitOfWorkMock;

        private List<Blog> _blogs;
        private BlogViewModel _fakeBlogViewModel;

        [TestInitialize]
        public void SetupContext()
        {
            mockUserManager = MockHelper.MockUserManager<IdentityUser>();
            _repository = new Mock<IBlogRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _blogs = new List<Blog>
            {
                new Blog { BlogTitle = "Blogg om katter", Description = "Velkommen til min blogg ", BlogId = 1, CanPost = true, OwnerId = "fc7457a7-4715-4d7e-afe2-81f3357b73c5", Created = new DateTime(2022, 06, 05) },
                new Blog { BlogTitle = "Matblogg", Description = "Oppskrifter", BlogId = 2, CanPost = false, OwnerId = "f5g4g7-4715-4d7e-afe2-81f3357b73c5", Created = new DateTime(2022, 06, 05) },
                new Blog { BlogTitle = "Norgesferie", Description = "Alt om reise i Norge", BlogId = 3, CanPost = true, OwnerId = "fc7457a7-4214-4d7e-afe2-81f3357b73c5", Created = new DateTime(2020, 05, 21) }
            };
            _fakeBlogViewModel = new BlogViewModel()
            {
                BlogId = 1,
                CanPost = true,
                BlogTitle = "Test",
                Description = "Desc",
                OwnerId = "f5g4g7-4715-4d7e-afe2-81f3357b73c5",
                Created = new DateTime(2020, 05, 21),
                Username = "Username"
            };
        }

        [TestMethod]
        public void IndexReturnsNotNullResult()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Blog.GetAll(null,null));
            var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object);

            // Act
            var result = (ViewResult) controller.Index();

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void CreateReturnsNotNullResult()
        {
            // Arrange
            var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object);

            // Act
            var result = (ViewResult)controller.Create();

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void SaveIsCalledWhenBlogIsCreated()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Save());
            var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object);

            // Act
            var result = controller.Create(new BlogViewModel());

            // Assert
            _repository.VerifyAll();
        }

        [TestMethod]
        public void CreateViewIsReturnedWhenInputIsNotValid()
        {
            // Arrange
            var viewModel = new BlogViewModel()
            {
                BlogTitle = "1",
                Description = ""
            };
            var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object);
            controller.ModelState.Clear();

            // Act
            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(viewModel, validationContext, validationResults, true);

            foreach (var validationResult in validationResults)
                controller.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);

            var result = controller.Create(viewModel);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(validationResults.Count > 0);
        }

        [TestMethod]
        public void DetailsCalledWithNoArgumentsReturnsANotFoundResult()
        {
            //Arrange
            var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object);

            //Act
            var result = controller.Details(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));

        }

        [TestMethod]
        public void DetailsCalledWithWrongBlogIdReturnsANotFoundResult()
        {
            //Arrange
            _unitOfWorkMock.Setup(x => x.Blog.GetBlogViewModel(42)).Returns(_fakeBlogViewModel);
            var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object)
            {
                ControllerContext = MockHelper.FakeControllerContext(true)
            };

            //Act
            var result = controller.Details(42);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));

        }

        [TestMethod]
        public void DetailsReturnsABlogViewModel()
        {
            //Arrange
            BlogViewModel blog = new BlogViewModel()
            {
                BlogId = 1,
                CanPost = true,
                BlogTitle = "Test",
                Description = "Desc",
                OwnerId = "f5g4g7-4715-4d7e-afe2-81f3357b73c5",
                Created = new DateTime(2020, 05, 21),
                Username = "Username"
            };
            //Blog blog = new Blog { BlogTitle = "Ny blogg", Description = "Velkommen til min blogg", BlogId = 1, CanPost = true, OwnerId = "fc7457a7-4214-4d7e-afe2-81f3357b73c5", Created = new DateTime(2020, 05, 21) };
            _unitOfWorkMock.Setup(x => x.Blog.GetBlogViewModel(1)).Returns(blog);
            var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object);

            //Act
            var result = (ViewResult)controller.Details(1);
            var blogReturn = result.ViewData.Model as BlogViewModel;

            //Assert
            Assert.AreEqual(blog, blogReturn, "Got wrong blog back");

        }

        [TestMethod]
        public void CreateReturnsAView()
        {
            //Arrange
            Blog blog = new Blog { BlogTitle = "Ny blogg", Description = "Velkommen til min blogg", BlogId = 1, CanPost = true };
            _repository.Setup(x => x.GetFirstOrDefault(y => y.BlogId == 1)).Returns(blog);
            var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object);

            //Act
            ActionResult result = controller.Create();

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void CreateReturnsInstanceOfBlogViewModel()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Blog.GetBlogViewModel(null)).Returns(_fakeBlogViewModel);
            //var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object);
            //_unitOfWorkMock.Setup(x => x.Blog.GetBlogViewModel(null)).Returns(_fakeBlogViewModel);
            var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object)
            {
                ControllerContext = MockHelper.FakeControllerContext(true)
            };

            // Act
            var result = (ViewResult)controller.Create();

            // Assert
            Assert.IsNotNull(result, "Result is null");
            Assert.AreSame(_fakeBlogViewModel, result.Model);
        }

        [TestMethod]
        public void GetAllBlogByUserReturnsCorrectBlog()
        {
            
        }

        [TestMethod]
        public void IndexReturnsAllBlogs()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Blog.GetAll(null, null)).Returns(_blogs);

            var controller = new BlogController(_unitOfWorkMock.Object, mockUserManager.Object);

            // Act
            var result = (ViewResult)controller.Index();

            // Assert
            CollectionAssert.AllItemsAreInstancesOfType((ICollection)result.ViewData.Model, typeof(BlogViewModel));
            Assert.IsNotNull(result, "View Result is null");

            var blogs = result.ViewData.Model as List<Blog>;
            Assert.AreEqual(3, _blogs.Count, "Got wrong number of blogs");
        }
    }
}
