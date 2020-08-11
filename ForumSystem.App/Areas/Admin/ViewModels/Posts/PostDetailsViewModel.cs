using ForumSystem.App.Areas.Admin.ViewModels.Comments;
using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.ViewModels.Posts
{
    public class PostDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public int TopicId { get; set; }

        public List<Comment> Comments { get; set; }

        public AllCommentsViewModel viewModel { get; set; }
    }
}
