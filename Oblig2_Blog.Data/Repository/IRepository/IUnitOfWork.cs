using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblig2_Blog.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IBlogRepository Blog { get; }
        IPostRepository Post { get; }
        ICommentRepository Comment { get; }
        ITagRepository Tag { get; }
        void Save();
    }
}
