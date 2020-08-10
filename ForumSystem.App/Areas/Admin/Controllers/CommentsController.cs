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
    [Area(GlobalConstants.AdminstrationRoleName)]
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
            var comment = await _services.GetComment(id);

            var model = new DetailsCommentViewModel
            {
                Id = id,
                Content = comment.Content,
                Author = comment.Author.UserName,
                Posted = comment.DateOfPost,
                ParentCommentId = comment.ParentCommentId,
                Replies = comment.Replies.ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment(string Content, string Author, int ParentCommentId)
        {

            var model = new AddReplyBindingModel
            {
                Author = Author,
                Content = Content,
                Posted = DateTime.UtcNow,
                ParentCommentId = ParentCommentId
            };

            await _services.AddReply(model);

            return RedirectToAction("GetAll", new { ParentCommentId = ParentCommentId });
        }


        public async Task<IActionResult> GetAll(int CommentId)
        {

            var comments = await _services.GetAllReplies(CommentId);

            var model = new AllCommentsViewModel
            {
                Comments = comments
            };

            return PartialView("~/Areas/Admin/Views/Shared/_Replies.cshtml", model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var comment = await _services.GetComment(id);



            var model = new DeleteCommentViewModel
            {
                Id = comment.Id,
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DeleteCommentViewModel model)
        {
            await _services.DeleteComment(model.Id);

            return RedirectToAction("Details", "Posts", new { id = model.PostId });
        }

    }
}