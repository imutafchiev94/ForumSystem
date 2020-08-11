using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumSystem.App.Areas.Admin.Services;
using ForumSystem.App.Areas.Admin.Services.Interfaces;
using ForumSystem.App.Areas.Admin.ViewModels.Comments;
using ForumSystem.App.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.App.Areas.Admin.Controllers
{
    [Route("[Area]/[Controller]/[Action]")]
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CommentsController : Controller
    {
        private readonly IAdminCommentsServices _services;

        public CommentsController(IAdminCommentsServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var comment = await _services.GetCommentAsync(id);

            var model = new DetailsCommentViewModel
            {
                Id = id,
                Content = comment.Content,
                Author = comment.Author.UserName,
                Posted = comment.DateOfPost,
                ParentCommentId = comment.ParentCommentId,
                Replies = comment.Replies.ToList(),
                viewModel = new AllCommentsViewModel
                {
                    Comments = _services.GetAllReplies(id)
                },
                PostId = comment.PostId
            };

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(int id,string Content, string Author, int ParentCommentId)
        {

            var model = new AddReplyBindingModel
            {
                Author = Author,
                Content = Content,
                Posted = DateTime.UtcNow,
                ParentCommentId = ParentCommentId,
            };

            await _services.AddReplyAsync(model);

            return RedirectToAction("GetAllReplies", new { CommentId = id });
        }


        public IActionResult GetAllReplies(int CommentId)
        {

            var comments = _services.GetAllReplies(CommentId);

            var model = new AllCommentsViewModel
            {
                Comments = comments
            };

            return PartialView("~/Areas/Admin/Views/Shared/_Replies.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, int PostId)
        {
            await _services.DeleteCommentAsync(id);

            return RedirectToAction("Details", "Posts", new { id = PostId });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,string Content, int PostId)
        {
            await _services.EditCommentAsync(Content, id);

            return RedirectToAction("Details", "Posts", new { id = PostId });
        }


        [HttpPost]
        public async Task<IActionResult> DeleteReply(int id)
        {
            var comment = await _services.GetCommentAsync(id);

            await _services.DeleteCommentAsync(id);

            return RedirectToAction("Details", "Comments", new { id = comment.ParentCommentId });
        }

    }
}