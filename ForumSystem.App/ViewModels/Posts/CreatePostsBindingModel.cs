using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.ViewModels.Posts
{
    public class CreatePostsBindingModel
    {

        public int TopicId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Posted { get; set; }

        public string Author { get; set; }
    }
}
