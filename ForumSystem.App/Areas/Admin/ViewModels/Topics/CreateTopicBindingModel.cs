using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.ViewModels.Topics
{
    public class CreateTopicBindingModel 
    {

        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
