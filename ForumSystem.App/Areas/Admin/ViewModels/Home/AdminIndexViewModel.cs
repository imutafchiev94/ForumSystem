using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.ViewModels.Home
{
    public class AdminIndexViewModel
    {

        public IEnumerable<Topic> Topics { get; set; }

    }
}
