using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ForumSystem.App.Models;
using ForumSystem.App.Services.Interface;
using ForumSystem.App.ViewModels.Home;

namespace ForumSystem.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeServices _service;

        public HomeController(IHomeServices service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var model = new IndexViewModel
            {
                Topics = await _service.GetAllTopics()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
