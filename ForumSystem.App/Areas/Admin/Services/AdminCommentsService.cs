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

        public Task DeleteComment(DeleteCommentViewModel model)
        {
            throw new NotImplementedException();
        }

        public Task EditComment(EditCommentBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetAllComments(int id)
        {
            var comments =  _dbContext.Comments.Where(c => c.PostId == id).Include(c => c.Author).OrderByDescending(c => c.DateOfPost).ToList();

            return comments;
        }
    }
}
