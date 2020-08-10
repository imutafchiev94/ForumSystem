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

       public Task<List<Post>> GetAllPosts(int id);

        public Task<Post> GetPost(int id);

        public Task CreatePost(CreatePostBindingModel model);

        public Task EditPost(EditPostBindingModel model);

        public Task DeletePost(DeletePostViewModel model);

    }
}
