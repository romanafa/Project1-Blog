using Oblig2_Blog.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblig2_Blog.Models.ViewModels
{
    public class TagViewModel
    {
        public int TagId { get; set; }
        [Required]
        public string TagName { get; set; }
        public IEnumerable<Post> Posts { get; set; }
    }
}
