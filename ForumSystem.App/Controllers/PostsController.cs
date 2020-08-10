using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ForumSystem.App.Areas.Admin.ViewModels.Posts;
using ForumSystem.App.Services.Interface;
using ForumSystem.App.ViewModels.Posts;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.App.Controllers
{
    public class PostsController : Controller
    {

        private readonly IPostsServices _services;

        public PostsController(IPostsServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IActionResult Create(int id)
        {

            var model = new CreatePostsBindingModel
            {
                TopicId = id,
                Author = User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostsBindingModel model)
        {
            model.Author = User.Identity.Name;

            await _services.CreatePost(model);

            return Redirect($"/Topics/Details/{model.TopicId}");
        }


    }
}