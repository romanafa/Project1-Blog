using Microsoft.AspNetCore.Identity;
using Oblig2_Blog.Data.Repository.IRepository;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Oblig2_Blog.Data.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {

        private ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _manager;

        public CommentRepository(ApplicationDbContext db, UserManager<IdentityUser> userManager) : base(db)
        {
            _db = db;
            _manager = userManager;
        }

        public void Update(Comment comment)
        {
            _db.Comments.Update(comment);
        }

        public async Task Save(CommentViewModel comment, IPrincipal principal, int? postId)
        {
            var currentUser = await _manager.FindByNameAsync(principal.Identity.Name);

            var c = new Comment
            {
                PostId = postId,
                CommentText = comment.CommentText,
                Created = DateTime.Now,
                User = currentUser
            };
            await _db.Comments.AddAsync(c);
            await _db.SaveChangesAsync();
        }

        public CommentViewModel GetCommentViewModel(int? commentId)
        {
            CommentViewModel c;
            if (commentId == null)
            {
                c = new CommentViewModel();
            }
            else
            {
                c = (from o in _db.Comments
                        where o.CommentId == commentId
                     select new CommentViewModel()
                        {
                            CommentId = o.CommentId,
                            CommentText = o.CommentText,
                            Created = o.Created,
                            OwnerId = o.OwnerId,
                            PostId = o.PostId
                        }
                    ).FirstOrDefault();
            }
            return c;
        }

        public IEnumerable<Comment> GetCommentPerPost(int? postId)
        {
            IEnumerable<Comment> comments =
                (IEnumerable<Comment>)(from c in _db.Comments where c.PostId == postId select c);
            foreach (var c in comments)
            {
                var commentOwnerUserName = (from u in _manager.Users
                    where u.Id.Equals(c.OwnerId)
                    select u.UserName).SingleOrDefault();
                c.Username = commentOwnerUserName;
            }
            
            return comments;
        }
    }
}
