using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Oblig2_Blog.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblig2_Blog.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _manager;

        public UnitOfWork(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _manager = userManager;
            Blog = new BlogRepository(_db, _manager);
            Post = new PostRepository(_db, _manager);
            Comment = new CommentRepository(_db, _manager);
            Tag = new TagRepository(_db);
        }

        public IBlogRepository Blog { get; private set; }
        public ICommentRepository Comment { get; private set; }
        public IPostRepository Post { get; private set; }
        public ITagRepository Tag { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
