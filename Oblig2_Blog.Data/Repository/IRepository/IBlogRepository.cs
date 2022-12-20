using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;

namespace Oblig2_Blog.Data.Repository.IRepository
{
    public interface IBlogRepository : IRepository<Blog>
    {
        BlogViewModel GetBlogViewModel(int? blogId);
        Task Save(BlogViewModel blog, IPrincipal principal);
        IEnumerable<Post> GetAllPosts(int? blogId);
    }
}
