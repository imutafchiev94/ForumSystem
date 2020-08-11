using ForumSystem.App.ViewModels.Posts;
using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Services.Interface
{
    public interface IPostsServices
    {
        public List<Post> GetAllPosts(int id);

        public Task<Post> GetPostAsync(int id);

        public Task CreatePostAsync(CreatePostsBindingModel model);

        public Task EditPostAsync(EditPostBindingModel model);

        public Task DeletePostAsync(DeletePostsViewModel model);

    }
}
