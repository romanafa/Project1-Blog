using Microsoft.AspNetCore.Identity;
using Oblig2_Blog.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace Oblig2_Blog.Models.ViewModels
{
    public class PostViewModel
    {
        public int PostId { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Tittelen må være mellom 5 - 30 bokstaver")]
        public string PostTitle { get; set; }
        [Required]
        public string PostText { get; set; }
        public string OwnerId { get; set; }
        public int? BlogId { get; set; }
        public virtual IdentityUser User { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public List<Tag> Tags { get; set; }

        public DateTime Created { get; set; }
        public string Username { get; set; }
    }
}
