using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.ViewModels.Comments
{
    public class AddCommentBindingModel
    {

        public int PostId { get; set; }

        [Required]
        [MinLength(10, ErrorMessage = "Comment must be at least 10 symbols")]
        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime Posted { get; set; }

    }
}
