using ForumSystem.App.Areas.Admin.ViewModels.Posts;
using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.Services.Interfaces
{
    public interface IAdminPostsService
    {

       public Task<List<Post>> GetAllPostsAsync(int id);

        public Task<Post> GetPostAsync(int id);

        public Task CreatePostAsync(CreatePostBindingModel model);

        public Task EditPostAsync(EditPostBindingModel model);

        public Task DeletePostAsync(DeletePostViewModel model);

    }
}
