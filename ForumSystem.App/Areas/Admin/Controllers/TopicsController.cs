using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumSystem.App.Areas.Admin.Services.Interfaces;
using ForumSystem.App.Areas.Admin.ViewModels.Topics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.App.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("[Area]/[Controller]/[Action]")]
    [Authorize]
    public class TopicsController : Controller
    {

        private readonly IAdminTopicService _service;
        private readonly IAdminPostsService _adminPostsService;

        public TopicsController(IAdminTopicService service, IAdminPostsService postsService)
        {
            _service = service;
            _adminPostsService = postsService;
        }

        [Route("/Topics/Create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Route("/Topics/Create")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTopicBindingModel model)
        {
            await _service.CreateTopicAsync(model);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var topic = await _service.GetTopicAsync(id);

            var viewModel = new DetailsTopicViewModel
            {
                Id = topic.Id,
                Title = topic.Title,
                Content = topic.Content,
                AuthorUserName = topic.Author.UserName,
                CreatedOn = topic.CreatedOn,
                Posts = await _adminPostsService.GetAllPosts(id)
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var topic = await _service.GetTopicAsync(id);

            var model = new EditTopicBindingModel
            {
                Id = id,
                Title = topic.Title,
                Content = topic.Content
            };

            return this.View(model);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTopicBindingModel model)
        {
            var id = int.Parse(HttpContext.Request.Query["id"].ToString().Split().ToArray()[0]);

            await _service.EditTopicAsync(id, model);

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await _service.GetTopicAsync(id);

            var model = new DeleteTopicViewModel
            {
                Id = topic.Id,
                Title = topic.Title
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteTopicViewModel model)
        {
            await _service.DeleteTopicAsync(model);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult GetAddCommentPartial()
        {
            return PartialView("__AddCommentsPartialView");
        }
    }
}