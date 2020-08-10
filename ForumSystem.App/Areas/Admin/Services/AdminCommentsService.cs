using ForumSystem.App.Areas.Admin.Services.Interfaces;
using ForumSystem.App.Areas.Admin.ViewModels.Comments;
using ForumSystem.Data;
using ForumSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.Services
{
    public class AdminCommentsService : IAdminCommentsServices
    {
        private readonly ForumSystemDbContext _dbContext;

        public AdminCommentsService(ForumSystemDbContext dbContext)
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

        public async Task AddReply(AddReplyBindingModel model)
        {
            var author = await _dbContext.Users.FirstOrDefaultAsync(a => a.UserName == model.Author);
            var parent = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == model.ParentCommentId);

            var comment = new Comment
            {
                Content = model.Content,
                AuthorId = author.Id,
                DateOfPost = model.Posted,
                PostId = parent.PostId,
                ParentCommentId = model.ParentCommentId
            };

            await _dbContext.Comments.AddAsync(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteComment(int id)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
            comment.IsDelete = true;

            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync();
        }

        public Task<Comment> EditComment(string content, int id)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAllComments(int id)
        {
            var comments =  _dbContext.Comments.Where(c => c.PostId == id).Where(c => c.IsDelete == false).Include(c => c.Author).OrderByDescending(c => c.DateOfPost).ToList();

            return comments;
        }

        public async Task<List<Comment>> GetAllReplies(int id)
        {
            var comments = await _dbContext.Comments.Include(c => c.Author).Include(c => c.Post).Where(c => c.IsDelete == false)
                .Where(c => c.ParentCommentId == id).ToListAsync();

            return comments;
        }

        public async Task<Comment> GetComment(int id)
        {
            var comment = await _dbContext.Comments.Include(c => c.Author).Include(c => c.Post).FirstOrDefaultAsync(c => c.Id == id);

            return comment;
        }

    }
}
