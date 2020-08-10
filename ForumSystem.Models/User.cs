using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Comments = new List<Comment>();

            this.Posts = new List<Post>();

            this.Topics = new List<Topic>();
        }


        public IEnumerable<Comment> Comments { get; set; }

        public IEnumerable<Post> Posts { get; set; }

        public IEnumerable<Topic> Topics { get; set; }

    }
}
