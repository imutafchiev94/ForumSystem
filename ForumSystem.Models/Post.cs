using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ForumSystem.Models
{
    public class Post
    {

        public Post()
        {
            this.Comments = new List<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string AuhtorId { get; set; }

        public User Author { get; set; }

        public bool IsDelete { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreatedOn { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        [ForeignKey("Topic")]
        public int TopicId { get; set; }

        public Topic Topic { get; set; }

    }
}
