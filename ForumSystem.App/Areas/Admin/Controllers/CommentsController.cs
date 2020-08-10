using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumSystem.App.Areas.Admin.Services.Interfaces;
using ForumSystem.App.Areas.Admin.ViewModels.Comments;
using ForumSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.App.Areas.Admin.Controllers
{
    [Route("[Area]/[Controller]/[Action]")]
    [Area("Admin")]
    public class CommentsController : Controller
    {

        private readonly IAdminCommentsServices _service;

        public CommentsController(IAdminCommentsServices service)
        {
            _service = service;
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }


    }
}