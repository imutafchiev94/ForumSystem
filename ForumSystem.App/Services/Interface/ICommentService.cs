using ForumSystem.App.ViewModels.Comments;
using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Services.Interface
{
    public interface ICommentService
    {
        public List<Comment> GetAllComments(int id);

        public Task AddComment(AddCommentBindingModel model);

        public Task DeleteComment(DeleteCommentViewModel model);

    }
}
