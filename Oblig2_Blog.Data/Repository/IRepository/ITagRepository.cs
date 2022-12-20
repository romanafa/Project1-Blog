using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;

namespace Oblig2_Blog.Data.Repository.IRepository
{
    public interface ITagRepository : IRepository<Tag>
    {
        Task Save(TagViewModel tag);
    }
}
