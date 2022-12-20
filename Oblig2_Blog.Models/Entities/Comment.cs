using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oblig2_Blog.Models.Entities
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public virtual IdentityUser User { get; set; }
        public string CommentText { get; set; }

        public DateTime Created { get; set; }


        public virtual Post Post { get; set; }
        public int? PostId { get; set; }
        [NotMapped]
        public string PostTitle { get; set; }
        [NotMapped]
        public string Username { get; set; }
    }
}
