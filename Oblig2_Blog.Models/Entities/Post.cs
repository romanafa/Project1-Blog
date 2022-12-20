using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oblig2_Blog.Models.Entities
{
    public class Post
    {
        public Post()
        {
            this.Tags = new HashSet<Tag>();
        }
        [Key]
        public int PostId { get; set; }
        public string? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual IdentityUser User { get; set; }
        public string PostTitle { get; set; }
        public string PostText { get; set; }

        public int? BlogId { get; set; }

        public DateTime Created { get; set; }

        public virtual Blog Blog { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
