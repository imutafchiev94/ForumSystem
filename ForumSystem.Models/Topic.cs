using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ForumSystem.Models
{
    public class Topic
    {

        public Topic()
        {
            this.Posts = new List<Post>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuthorId { get; set; }

        public bool IsDelete { get; set; }

        public User Author { get; set; }
        
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        public IEnumerable<Post> Posts { get; set; }

    }
}
