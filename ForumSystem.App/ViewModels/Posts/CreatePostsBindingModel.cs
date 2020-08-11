using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.ViewModels.Posts
{
    public class CreatePostsBindingModel
    {

        public int TopicId { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 symbols")]
        public string Title { get; set; }

        [Required]
        [MinLength(50, ErrorMessage = "Content must be at least 50 symbols")]
        public string Content { get; set; }

        public DateTime Posted { get; set; }

        public string Author { get; set; }
    }
}
