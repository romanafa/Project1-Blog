using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oblig2_Blog.Models.Entities
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }
        public string? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual IdentityUser User { get; set; }
        public string BlogTitle { get; set; }
        public string Description { get; set; }
        public bool CanPost { get; set; }
        public DateTime Created { get; set; }

        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }
    }
}
