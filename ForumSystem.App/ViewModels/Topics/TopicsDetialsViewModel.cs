using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.ViewModels.Topics
{
    public class TopicsDetialsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public DateTime PostedOn { get; set; }

        public List<Post> Posts { get; set; }

    }
}
