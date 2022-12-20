using Oblig2_Blog.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace Oblig2_Blog.Data.Repository
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _manager;

        public BlogRepository(ApplicationDbContext db, UserManager<IdentityUser> userManager) : base(db)
        {
            _db = db;
            _manager = userManager;
        }

        public BlogViewModel GetBlogViewModel(int? blogId)
        {
            BlogViewModel b;

            if (blogId == null)
            {
                b = new BlogViewModel();
            }
            else
            {
                b = (from o in _db.Blogs
                     where o.BlogId == blogId
                        select new BlogViewModel()
                        {
                            BlogId = o.BlogId,
                            BlogTitle = o.BlogTitle,
                            Description = o.Description,
                            CanPost = o.CanPost,
                            Created = o.Created,
                            OwnerId = o.OwnerId
                        }
                    ).First();
            }
            return b;
        }

        public async Task Save(BlogViewModel blog, IPrincipal principal)
        {
            var currentUser = await _manager.FindByNameAsync(principal.Identity.Name);
            var b = new Blog
            {
                BlogTitle = blog.BlogTitle,
                Description = blog.Description,
                CanPost = blog.CanPost,
                Created = DateTime.Now,
                User = currentUser
            };
            await _db.Blogs.AddAsync(b);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Post> GetAllPosts(int? blogId)
        {
            IEnumerable<Post> posts;
            posts = (from p in _db.Posts where p.BlogId == blogId select p).ToList();
            return posts;
        }
    }
}
