using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Oblig2_Blog.Data.Repository.IRepository;
using Oblig2_Blog.Models.Entities;
using Oblig2_Blog.Models.ViewModels;

namespace Oblig2_Blog.Data.Repository
{
    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private ApplicationDbContext _db;

        public TagRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task Save(TagViewModel tag)
        {
            var t = new Tag
            {
                TagName = tag.TagName
            };
            await _db.Tags.AddAsync(t);
            await _db.SaveChangesAsync();
        }
    }
}
