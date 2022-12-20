using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
    public class CommentControllerTest
    {
        Mock<ICommentRepository> _repository;
        Mock<UserManager<IdentityUser>> mockUserManager;
        Mock<IUnitOfWork> _unitOfWorkMock;

        private List<Comment> _comments;
        private List<Post> _posts;
        private CommentViewModel _fakeCommentViewModel;

        [TestInitialize]
        public void SetupContext()
        {
            mockUserManager = MockHelper.MockUserManager<IdentityUser>();
            _repository = new Mock<ICommentRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();


            _comments = new List<Comment>
            {
                new Comment { CommentText = "Kommentar 1", CommentId = 1, PostId = 1, Created = new DateTime(2022, 06, 05), OwnerId = "8aa46eaa-5727-42b7-8f3f-16a96d894658"},
                new Comment { CommentText = "Kommentar 2", CommentId = 2, PostId = 1, Created = new DateTime(2022, 02, 13), OwnerId = "379bb5e6-6292-4e1e-8f84-47fec88eff93"},
                new Comment { CommentText = "Kommentar 3", CommentId = 3, PostId = 1, Created = new DateTime(2021, 04, 21), OwnerId = "fc7457a7-4715-4d7e-afe2-81f3357b73c5"}
            };
            _fakeCommentViewModel = new CommentViewModel()
            {
                CommentId = 1,
                PostId = 1,
                CommentText = "Test",
                Created = new DateTime(2021, 04, 21),
                OwnerId = "fc7457a7-4715-4d7e-afe2-81f3357b73c5"

            };
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
        }

        // Create unit tests and SaveIsCalledWhenCommentIsCreated() fail because of ViewBag null pointer exception
        [TestMethod]
        public void CreateViewIsReturnedWhenInputIsNotValid()
        {
            // Arrange
            var viewModel = new CommentViewModel()
            {
                CommentText = "",
            };
            var controller = new CommentController(_unitOfWorkMock.Object, mockUserManager.Object);

            // Act
            var validationContext = new ValidationContext(viewModel, null, null);
            var validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(viewModel, validationContext, validationResults, true);
            foreach (var validationResult in validationResults)
                controller.ModelState.AddModelError(validationResult.MemberNames.First(),
                    validationResult.ErrorMessage);

            //var result = controller.Create(1, viewModel) as IActionResult;
            var result = controller.Create(1, viewModel);
            // Assert

            Assert.IsNotNull(result);
            Assert.IsTrue(validationResults.Count > 0);
        }

        [TestMethod]
        public void CreateReturnsNotNullResult()
        {
            // Arrange
            var controller = new CommentController(_unitOfWorkMock.Object, mockUserManager.Object);
            var post = new Post();
            _unitOfWorkMock.Setup(x => x.Post.GetFirstOrDefault(p => p.PostId == 1)).Returns(post);
            controller.ViewData["PostTitle"] = post.PostTitle;

            // Act
            var result = (ViewResult)controller.Create(1);

            // Assert
            Assert.IsNotNull(result, "View Result is null");
        }

        [TestMethod]
        public void CreateReturnsAView()
        {
            //Arrange
            Comment comment = new Comment { CommentText = "Hallo", CommentId = 1, PostId = 1, OwnerId = "fc7457a7-4715-4d7e-afe2-81f3357b73c5", Created = new DateTime(2022, 06, 05) };
            _repository.Setup(x => x.GetFirstOrDefault(y => y.CommentId == 1)).Returns(comment);
            var controller = new CommentController(_unitOfWorkMock.Object, mockUserManager.Object);
            var post = new Post();
            _unitOfWorkMock.Setup(x => x.Post.GetFirstOrDefault(p => p.PostId == 1)).Returns(post);
            controller.ViewData["PostTitle"] = post.PostTitle;
            //Act
            ActionResult result = controller.Create(1);

            //Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));

        }

        [TestMethod]
        public void CreateReturnsInstanceOfCommentViewModel()
        {
            // Arrange
            _repository.Setup(x => x.GetCommentViewModel(null)).Returns(_fakeCommentViewModel);
            var controller = new CommentController(_unitOfWorkMock.Object, mockUserManager.Object)
            {
                ControllerContext = MockHelper.FakeControllerContext(true)
            };
            var post = new Post();
            _unitOfWorkMock.Setup(x => x.Post.GetFirstOrDefault(p => p.PostId == 1)).Returns(post);
            controller.ViewData["PostTitle"] = post.PostTitle;

            // Act
            var result = (ViewResult)controller.Create(1);
            // Assert
            Assert.IsNotNull(result, "Result is null");
            Assert.AreSame(_fakeCommentViewModel, result.Model);
        }

        [TestMethod]
        public void SaveIsCalledWhenCommentIsCreated()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Save());
            var post = new Post();
            _unitOfWorkMock.Setup(x => x.Post.GetFirstOrDefault(p => p.PostId == 1)).Returns(post);
            var controller = new CommentController(_unitOfWorkMock.Object, mockUserManager.Object);
            controller.ViewData["PostTitle"] = post.PostTitle;

            // Act
            var result = controller.Create(1);

            // Assert
            _repository.VerifyAll();

        }

        [TestMethod]
        public void DeleteCalledWithNoArgumentsReturnsANotFoundResult()
        {
            //Arrange
            var controller = new CommentController(_unitOfWorkMock.Object, mockUserManager.Object);

            //Act
            var result = controller.Delete(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void DeleteCalledWithWrongCommentIdReturnsANotFoundResult()
        {
            //Arrange
            _unitOfWorkMock.Setup(x => x.Comment.GetFirstOrDefault(y => y.CommentId == 1)).Returns<Comment>(null);
            var controller = new CommentController(_unitOfWorkMock.Object, mockUserManager.Object);
            
            //Act
            var result = controller.Delete(2);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));

        }

        [TestMethod]
        public void DeleteConfirmedCallsDeleteInIRepository()
        {
            //Arrange
            Comment comment = new Comment { CommentText = "Hallo", CommentId = 1, PostId = 1, OwnerId = "fc7457a7-4715-4d7e-afe2-81f3357b73c5", Created = new DateTime(2022, 06, 05) };
            _unitOfWorkMock.Setup(x => x.Comment.GetFirstOrDefault(y => y.CommentId == 1)).Returns(comment);
            var controller = new CommentController(_unitOfWorkMock.Object, mockUserManager.Object);
            controller.ModelState.Clear();
            //Act
            controller.DeleteConfirmed(1, 1);

            // Assert
            _unitOfWorkMock.Verify(x => x.Comment.Remove(comment));

        }

    }
}
