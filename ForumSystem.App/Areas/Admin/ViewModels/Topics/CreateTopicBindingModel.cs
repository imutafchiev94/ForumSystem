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

        [Required]
        [MinLength(5, ErrorMessage = "Title must be at least 5 symbols")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [MinLength(50, ErrorMessage = "Content must be at least 50 symbols")]
        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
