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
    public interface ICommentRepository : IRepository<Comment>
    {
        void Update(Comment comment);
        Task Save(CommentViewModel comment, IPrincipal principal, int? postId);
        CommentViewModel GetCommentViewModel(int? commentId);
        IEnumerable<Comment> GetCommentPerPost(int? postId);
    }
}
