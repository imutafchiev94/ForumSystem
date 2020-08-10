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

        public async Task CreatePost(CreatePostBindingModel model)
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

        public async Task DeletePost(DeletePostViewModel model)
        {
            var post = await GetPost(model.Id);
            post.IsDelete = true;

            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditPost(EditPostBindingModel model)
        {
            var post = await GetPost(model.Id);

            post.Title = model.Title;
            post.Content = model.Content;

            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }


        public async Task<List<Post>> GetAllPosts(int id)
        {

            var posts = await _dbContext.Posts.Include(p => p.Author).Where(p => p.TopicId == id).Where(p => p.IsDelete == false).ToListAsync();

            return posts;
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await _dbContext.Posts.Include(p => p.Author).Include(p => p.Comments).Where(p => p.IsDelete == false).FirstOrDefaultAsync(p => p.Id == id);

            if(post == null)
            {
                throw new NullReferenceException($"Post with {id} doesn't exist");
            }

            return post;
        }
    }
}
