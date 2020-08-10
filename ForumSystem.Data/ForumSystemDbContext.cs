
using ForumSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumSystem.Data
{
    public class ForumSystemDbContext : IdentityDbContext
    {
        public ForumSystemDbContext(DbContextOptions<ForumSystemDbContext> options) : base(options)
        {

        }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Topic> Topics { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Comment>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments)
                .HasForeignKey(c => c.PostId);

            builder.Entity<Comment>()
                .HasOne(c => c.Author)
                .WithMany(a => a.Comments)
                .HasForeignKey(c => c.AuthorId);

            builder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.AuhtorId);

            builder.Entity<Post>()
                .HasOne(p => p.Topic)
                .WithMany(t => t.Posts)
                .HasForeignKey(p => p.TopicId);

            builder.Entity<Topic>()
                .HasOne(t => t.Author)
                .WithMany(a => a.Topics)
                .HasForeignKey(t => t.AuthorId);

            base.OnModelCreating(builder);
        }
    }
}
