using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ForumSystem.App.Areas.Admin.Services.Interfaces;
using ForumSystem.App.Areas.Admin.ViewModels.Home;
using ForumSystem.App.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ForumSystem.App.Areas.Admin.Controllers
{
    [Area(GlobalConstants.AdminstrationRoleName)]
    [Authorize(Roles ="Admin")]
    public class HomeController : Controller
    {

        private readonly IAdminTopicService _service;

        public HomeController(IAdminTopicService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("/Admin/Index")]
        public async Task<IActionResult> Index()
        {
            var model = new AdminIndexViewModel
            {
                Topics = await _service.GetAllTopicsAsync()
            };

            return View(model);
        }
    }
}