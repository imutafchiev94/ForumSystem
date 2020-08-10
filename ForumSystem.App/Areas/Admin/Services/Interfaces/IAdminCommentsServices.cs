using ForumSystem.App.Areas.Admin.ViewModels.Comments;
using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.Services.Interfaces
{
    public interface IAdminCommentsServices
    {
        public List<Comment> GetAllComments(int id);

        public Task AddComment(AddCommentBindingModel model);

        public Task EditComment(EditCommentBindingModel model);

        public Task DeleteComment(DeleteCommentViewModel model);
    }
}
