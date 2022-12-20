using System.ComponentModel.DataAnnotations;

namespace Oblig2_Blog.Models.ViewModels
{
    public class CommentViewModel
    {
        public int CommentId { get; set; }
        [Required]
        public string CommentText { get; set; }

        public string OwnerId { get; set; }

        public int? PostId { get; set; }
        public int? BlogId { get; set; }
        public DateTime Created { get; set; }
        public string Username { get; set; }
        public string PostTitle { get; set; }
    }
}
