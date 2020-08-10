using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumSystem.App.Services;
using ForumSystem.App.Services.Interface;
using ForumSystem.App.ViewModels.Topics;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.App.Controllers
{
    public class TopicsController : Controller
    {

        private readonly ITopicsServices _service;
        private readonly IPostsServices _postsServices;


        public TopicsController(ITopicsServices service, IPostsServices postsServices)
        {
            _service = service;
            _postsServices = postsServices;
        }

        public async Task<IActionResult> Details(int id)
        {
            var topic = await _service.GetTopic(id);

            var model = new TopicsDetialsViewModel
            {
                Id = topic.Id,
                Title = topic.Title,
                Author = topic.Author.UserName,
                Content = topic.Content,
                PostedOn = topic.CreatedOn,
                Posts = await _postsServices.GetAllPosts(id)
            };

            return View(model);
        }
    }
}