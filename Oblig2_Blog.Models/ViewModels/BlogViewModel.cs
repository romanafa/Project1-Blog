using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Oblig2_Blog.Models.Entities;

namespace Oblig2_Blog.Models.ViewModels
{
    public class BlogViewModel
    {
        public int BlogId { get; set; }
        [Required]
        public string BlogTitle { get; set; }
        [Required]
        public string Description { get; set; }
        public bool CanPost { get; set; }
        public string OwnerId { get; set; }
        public IdentityUser User { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public DateTime Created { get; set; }
        public string Username { get; set; }
    }
}
