using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblig2_Blog.Models.Entities
{
    public class Tag
    {
        public Tag()
        {
            this.Posts = new HashSet<Post>();
        }
        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
