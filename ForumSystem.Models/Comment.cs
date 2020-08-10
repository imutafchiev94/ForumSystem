using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace ForumSystem.Models
{
    public class Comment
    {

        public Comment()
        {
            this.Replies = new List<Comment>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateOfPost { get; set; }

        public string AuthorId { get; set; }

        [Required]
        public User Author { get; set; }

        public IEnumerable<Comment> Replies { get; set; }

        [ForeignKey("Post")]
        public int PostId { get; set; }

        public Post Post { get; set; }
    }
}
