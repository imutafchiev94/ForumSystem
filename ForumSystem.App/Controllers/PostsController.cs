using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ForumSystem.App.Services.Interface;
using ForumSystem.App.ViewModels.Comments;
using ForumSystem.App.ViewModels.Posts;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.App.Controllers
{
    public class PostsController : Controller
    {

        private readonly IPostsServices _services;
        private readonly ICommentService _commentService;

        public PostsController(IPostsServices services, ICommentService commentService)
        {
            _services = services;
            _commentService = commentService;
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
            if(ModelState.IsValid)
            {
                model.Author = User.Identity.Name;

                await _services.CreatePostAsync(model);

                return Redirect($"/Topics/Details/{model.TopicId}");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _services.GetPostAsync(id);

            var model = new PostsDetailsViewModel
            {
                Id = id,
                Author = post.Author.UserName,
                Title = post.Title,
                Content = post.Content,
                CreatedOn = post.CreatedOn,
                Comments = _commentService.GetAllComments(id),
                TopicId = post.TopicId,
                viewModel = new AllCommentsViewModel { Comments = _commentService.GetAllComments(id) }
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

            await _commentService.AddCommentAsync(model);

            return RedirectToAction("GetAll", new { PostId = PostId });
        }


        public IActionResult GetAll(int Postid)
        {

            var comments = _commentService.GetAllComments(Postid);

            var model = new AllCommentsViewModel
            {
                Comments = comments
            };

            return PartialView("~/Views/Shared/_Comments.cshtml", model);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _services.GetPostAsync(id);

            if (User.Identity.Name != post.Author.UserName)
            {
                return RedirectToAction("Index", "Home");
            }

            var model = new EditPostBindingModel
            {
                Id = id,
                Title = post.Title,
                Content = post.Content,
                TopicId = post.TopicId
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditPostBindingModel model)
        {
            if(ModelState.IsValid)
            {
                await _services.EditPostAsync(model);

                return RedirectToAction("Details", new { id = model.Id });

            }    
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _services.GetPostAsync(id);



            var model = new DeletePostsViewModel
            {
                Id = post.Id,
                Title = post.Title
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeletePostsViewModel model)
        {
            await _services.DeletePostAsync(model);

            return RedirectToAction("Details", "Topics", new { id = model.TopicId });
        }


    }
}