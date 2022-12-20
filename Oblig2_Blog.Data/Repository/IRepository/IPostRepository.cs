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
    public interface IPostRepository : IRepository<Post>
    {
        void Update(Post post);
        PostViewModel GetPostViewModel(int? postId);
        Task Save(PostViewModel post, IPrincipal principal, int? blogId);
        IEnumerable<Comment> GetAllComments(int? postId);
        IEnumerable<Tag> GetAllTags(int? postId);
    }
}
