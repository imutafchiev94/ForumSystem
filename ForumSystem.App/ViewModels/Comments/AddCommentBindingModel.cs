using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.ViewModels.Comments
{
    public class AddCommentBindingModel
    {
        public int PostId { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime Posted { get; set; }
    }
}
