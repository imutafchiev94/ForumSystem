using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.ViewModels.Posts
{
    public class DeletePostsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int TopicId { get; set; }
    }
}
