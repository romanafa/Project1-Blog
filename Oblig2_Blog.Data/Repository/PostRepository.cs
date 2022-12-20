using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oblig2_Blog.Data.Repository.IRepository;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Oblig2_Blog.Data.Repository 
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _manager;

        public PostRepository(ApplicationDbContext db, UserManager<IdentityUser> userManager) : base(db)
        {
            _db = db;
            _manager = userManager;
        }

        public void Update(Post post)
        {
            _db.Posts.Update(post);
        }

        public PostViewModel GetPostViewModel(int? id)
        {
            PostViewModel p;
            if (id == null)
            {
                p = new PostViewModel();
            }
            else
            {
                p = (from o in _db.Posts
                        where o.PostId == id
                        select new PostViewModel()
                        {
                            PostId = o.PostId,
                            PostTitle = o.PostTitle,
                            PostText = o.PostText,
                            Created = o.Created,
                            OwnerId = o.OwnerId,
                            BlogId = o.BlogId
                        }
                    ).First();
            }
            return p;
        }

        public async Task Save(PostViewModel post, IPrincipal principal, int? blogId)
        {
            var currentUser = await _manager.FindByNameAsync(principal.Identity.Name);

            var p = new Post
            {
                BlogId = blogId,
                PostTitle = post.PostTitle,
                PostText = post.PostText,
                Created = DateTime.Now,
                User = currentUser,
            };

            await _db.Posts.AddAsync(p);
            await _db.SaveChangesAsync();
        }

        public IEnumerable<Comment> GetAllComments(int? postId)
        {
            IEnumerable<Comment> comments;
            comments = (from c in _db.Comments where c.PostId == postId select c).ToList();
            return comments;
        }

        public IEnumerable<Tag> GetAllTags(int? postId)
        {
            IEnumerable<Tag> tags;
            tags = _db.Posts.Where(p => p.PostId == 1).SelectMany(t => t.Tags).ToList();
            return tags;
        }
    }
}
