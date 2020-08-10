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
        public Task<List<Post>> GetAllPosts(int id);

        public Task<Post> GetPost(int id);

        public Task CreatePost(CreatePostsBindingModel model);

    }
}
