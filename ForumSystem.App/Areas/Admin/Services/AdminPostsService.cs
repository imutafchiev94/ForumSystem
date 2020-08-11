using ForumSystem.App.Areas.Admin.Services.Interfaces;
using ForumSystem.App.Areas.Admin.ViewModels.Posts;
using ForumSystem.Data;
using ForumSystem.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.Services
{
    public class AdminPostsService : IAdminPostsService
    {

        private readonly ForumSystemDbContext _dbContext;

        public AdminPostsService(ForumSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreatePostAsync(CreatePostBindingModel model)
        {
            var author = await _dbContext.Users.FirstOrDefaultAsync(a => a.UserName == model.Author);

            if(author == null)
            {
                throw new NullReferenceException("User with this userName doesn't exist");
            }

            var post = new Post
            {
                Title = model.Title,
                AuhtorId = author.Id,
                Content = model.Content,
                TopicId = model.TopicId,
                CreatedOn = DateTime.UtcNow
            };

            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePostAsync(DeletePostViewModel model)
        {
            var post = await GetPostAsync(model.Id);

            if (post == null)
            {
                throw new NullReferenceException($"Post with {model.Id} doesn't exist");
            }

            post.IsDelete = true;

            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditPostAsync(EditPostBindingModel model)
        {
            var post = await GetPostAsync(model.Id);

            if (post == null)
            {
                throw new NullReferenceException($"Post with {model.Id} doesn't exist");
            }

            post.Title = model.Title;
            post.Content = model.Content;

            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<List<Post>> GetAllPostsAsync(int id)
        {

            var posts = await _dbContext.Posts.Include(p => p.Author).Where(p => p.TopicId == id)
                .Where(p => p.IsDelete == false).OrderByDescending(p => p.CreatedOn).ToListAsync();

            return posts;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            var post = await _dbContext.Posts.Include(p => p.Author).Include(p => p.Comments).Include(p => p.Topic)
                .Where(p => p.IsDelete == false).FirstOrDefaultAsync(p => p.Id == id);

            if(post == null)
            {
                throw new NullReferenceException($"Post with {id} doesn't exist");
            }

            return post;
        }
    }
}
