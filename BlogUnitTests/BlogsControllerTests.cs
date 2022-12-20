using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using Oblig2_Blog.Data;
using Oblig2_Blog.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oblig2_Blog.Data.Repository;
using Oblig2_Blog.Models.ViewModels;
using Xunit;
using Assert = Xunit.Assert;
using System.Security.Claims;

namespace BlogUnitTests
{
    public abstract class BlogsControllerTests
    {
        #region Seeding

        protected BlogsControllerTests(DbContextOptions<ApplicationDbContext> contextOptions)
        {
            ContextOptions = contextOptions;

            Seed();
        }

        protected DbContextOptions<ApplicationDbContext> ContextOptions { get; }

        private void Seed()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Blogs.AddRange(
                    new Blog
                    {
                        BlogTitle = "Blogg om katter",
                        Description = "Velkommen til min blogg ",
                        CanPost = true,
                        Created = new DateTime(2022, 02, 13),
                        OwnerId = "379bb5e6-6292-4e1e-8f84-47fec88eff93"
                    },
                    new Blog
                    {
                        BlogTitle = "Matblogg",
                        Description = "Oppskrifter",
                        CanPost = false,
                        Created = new DateTime(2022, 05, 28),
                        OwnerId = "607ee2ca-4326-43d8-b289-480a94013948"
                    },
                    new Blog
                    {
                        BlogTitle = "Norgesferie",
                        Description = "Alt om reise i Norge",
                        CanPost = true,
                        Created = new DateTime(2022, 06, 04),
                        OwnerId = "2a093558-1d1f-4c77-8422-aad80e5d168b"
                    }
                    );

                context.SaveChanges();
            }
        }
        #endregion

        [Fact]
        public async Task CanGetAllBlogs()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                //Arrange

                Mock<UserManager<IdentityUser>> mockUserManager;
                mockUserManager = MockHelper.MockUserManager<IdentityUser>();
                var repository = new BlogRepository(context, mockUserManager.Object);
                //Act
                var result = repository.GetAll();

                //Assert
                Assert.Equal(6, result.Count());
                var blogs = result as List<Blog>;
                Assert.Equal("Blogg om katter", blogs[0].BlogTitle);
                Assert.Equal("Matblogg", blogs[1].BlogTitle);
                Assert.Equal("Norgesferie", blogs[2].BlogTitle);
            }
        }

        [Fact]
        public void CanGetBlog()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                //Arrange
                Mock<UserManager<IdentityUser>> mockUserManager;
                mockUserManager = MockHelper.MockUserManager<IdentityUser>();
                var repository = new BlogRepository(context, mockUserManager.Object);
                //Act
                var item = repository.GetFirstOrDefault(x => x.BlogId == 2);
                //Assert
                Assert.Equal("Matblogg", item.BlogTitle);
            }
        }

        [Fact]
        public void CanSaveProduct()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                //Arrange
                Mock<UserManager<IdentityUser>> mockUserManager;
                mockUserManager = MockHelper.MockUserManager<IdentityUser>();
                var repository = new BlogRepository(context, mockUserManager.Object);
                var blog = new BlogViewModel
                {
                    BlogTitle = "Biler",
                    Description = "Alt om reise i biler",
                    CanPost = true,
                    Created = new DateTime(2022, 06, 04),
                    OwnerId = "2a09t667-1d1f-4c77-8422-aad80e5d168b",
                    BlogId = 7,
                };
                //Act
                repository.Save(blog, ClaimsPrincipal.Current);
                //Assert
                Assert.NotEqual(0, blog.BlogId);

            }
        }

        [Fact]
        public void CanRemoveBlog()
        {
            using (var context = new ApplicationDbContext(ContextOptions))
            {
                //Arrange
                Mock<UserManager<IdentityUser>> mockUserManager;
                mockUserManager = MockHelper.MockUserManager<IdentityUser>();
                var repository = new BlogRepository(context, mockUserManager.Object);
                var blog = new Blog { BlogId = 1 };
                //Act
                repository.Remove(blog);
                //Assert
                Assert.False(context.Set<Blog>().Any(e => e.BlogTitle == "Ny blogg"));
            }
        }

    }
}
