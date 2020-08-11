
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

        public async Task AddCommentAsync(AddCommentBindingModel model)
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

        public async Task AddReplyAsync(AddReplyBindingModel model)
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

            parent.Replies.ToList().Add(comment);
            await _dbContext.Comments.AddAsync(comment);
            _dbContext.Comments.Update(parent);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int id)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                throw new NullReferenceException($"Comment with {id} doesn't exist");
            }

            comment.IsDelete = true;

            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Comment> EditCommentAsync(string content, int id)
        {
            var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                throw new NullReferenceException($"Comment with {id} doesn't exist");
            }

            comment.Content = content;

            _dbContext.Comments.Update(comment);
            await _dbContext.SaveChangesAsync();

            return comment;
        }

        public List<Comment> GetAllComments(int id)
        {
            var comments = _dbContext.Comments.Where(c => c.PostId == id).Where(c => c.IsDelete == false).
                Where(c => c.ParentCommentId == 0).Include(c => c.Author).OrderByDescending(c => c.DateOfPost).ToList();

            return comments;
        }

        public List<Comment> GetAllReplies(int id)
        {
            var comments = _dbContext.Comments.Include(c => c.Author).Include(c => c.Post).Where(c => c.IsDelete == false)
                .Where(c => c.ParentCommentId == id).OrderByDescending(c => c.DateOfPost).ToList();

            return comments;
        }

        public async Task<Comment> GetCommentAsync(int id)
        {
            var comment = await _dbContext.Comments.Include(c => c.Author).Include(c => c.Post).FirstOrDefaultAsync(c => c.Id == id);

            if (comment == null)
            {
                throw new NullReferenceException($"Comment with {id} doesn't exist");
            }

            return comment;
        }
    }
}
