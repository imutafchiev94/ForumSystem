using ForumSystem.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.ViewModels.Comments
{
    public class DetailsCommentViewModel
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public DateTime Posted { get; set; }

        public string Author { get; set; }

        public int PostId { get; set; }

        public int ParentCommentId { get; set; }

        public List<Comment> Replies { get; set; }

        public AllCommentsViewModel viewModel { get; set; }
    }  
}
