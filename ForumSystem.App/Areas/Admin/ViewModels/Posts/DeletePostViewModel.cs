using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.ViewModels.Posts
{
    public class DeletePostViewModel
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public int TopicId { get; set; }
    }
}
