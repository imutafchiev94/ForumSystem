using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumSystem.App.Areas.Admin.Services.Interfaces;
using ForumSystem.App.Areas.Admin.ViewModels.Comments;
using ForumSystem.App.Areas.Admin.ViewModels.Posts;
using ForumSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ForumSystem.App.Areas.Admin.Controllers
{



    [Route("[Area]/[Controller]/[Action]")]
    [Area("Admin")]
    public class PostsController : Controller
    {
        private readonly IAdminPostsService _service;
        private readonly IAdminCommentsServices _adminCommentService;

        public PostsController(IAdminPostsService service, IAdminCommentsServices adminCommentService)
        {
            _service = service;
            _adminCommentService = adminCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {


            var model = new CreatePostBindingModel
            {
                TopicId = id,
                Author = User.Identity.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostBindingModel model)
        {
            model.Author = User.Identity.Name;

            var id = int.Parse(HttpContext.Request.Query["id"].ToString().Split().ToArray()[0]);

            model.TopicId = id;

            await _service.CreatePost(model);

            return Redirect($"~/Admin/Topics/Details/?id={model.TopicId}");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _service.GetPost(id);

            var model = new PostDetailsViewModel
            {
                Id = id,
                Author = post.Author.UserName,
                Title = post.Title,
                Content = post.Content,
                CreatedOn = post.CreatedOn,
                Comments =  _adminCommentService.GetAllComments(id),
                viewModel = new AllCommentsViewModel { Comments = _adminCommentService.GetAllComments(id)}
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(string Content, string Author, DateTime Posted, int PostId)
        {

            var model = new AddCommentBindingModel
            {
                Author = Author,
                Content = Content,
                Posted = DateTime.UtcNow,
                PostId = PostId
            };

            await _adminCommentService.AddComment(model);

            return RedirectToAction("GetAll", new { PostId = PostId });
        }


        public IActionResult GetAll(int Postid)
        {

            var comments = _adminCommentService.GetAllComments(Postid);

            var model = new AllCommentsViewModel
            {
                Comments = comments
            };

            return PartialView("~/Areas/Admin/Views/Shared/_Comments.cshtml", model);
        }

        [HttpGet]
        
        public async Task<IActionResult> Edit(int id)
        {
            var post = await _service.GetPost(id);

            if(User.Identity.Name != post.Author.UserName)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new EditPostBindingModel
            {
                Id = id,
                Title = post.Title,
                Content = post.Content
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostBindingModel model)
        {
            await _service.EditPost(model);

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _service.GetPost(id);



            var model = new DeletePostViewModel
            {
                Id = post.Id,
                Title = post.Title
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePostViewModel model)
        {
            await _service.DeletePost(model);

            return RedirectToAction("Details", "Topics", new { id = model.TopicId});
        }

    }
}
