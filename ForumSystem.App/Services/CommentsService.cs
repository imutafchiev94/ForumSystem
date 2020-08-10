
using ForumSystem.App.Services.Interface;
using ForumSystem.App.ViewModels.Comments;
using ForumSystem.Data;
using ForumSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Services
{
    public class CommentsService : ICommentService
    {
        private readonly ForumSystemDbContext _dbContext;

        public CommentsService(ForumSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddComment(AddCommentBindingModel model)
        {
            var author = await _dbContext.Users.FirstOrDefaultAsync(a => a.UserName == model.Author);

            var comment = new Comment
            {
                Content = model.Content,
                AuthorId = author.Id,
                DateOfPost = model.Posted,
                PostId = model.PostId,
            };

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteComment(DeleteCommentViewModel model)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == model.Id);
            comment.IsDelete = false;

            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync();
        }

        public List<Comment> GetAllComments(int id)
        {
            var comments = _dbContext.Comments.Where(c => c.PostId == id).Where(c => c.IsDelete == false).Include(c => c.Author).OrderByDescending(c => c.DateOfPost).ToList();

            return comments;
        }

        public async Task<Comment> GetComment(int id)
        {
            var comment = await _dbContext.Comments.Include(c => c.Author).Include(c => c.Post).FirstOrDefaultAsync(c => c.Id == id);

            return comment;
        }
    }
}
