using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Services.Interface
{
    public interface IHomeServices
    {

        public Task<List<Topic>> GetAllTopics();

    }
}
