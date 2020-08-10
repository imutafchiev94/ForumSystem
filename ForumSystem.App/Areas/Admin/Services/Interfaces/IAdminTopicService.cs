using ForumSystem.App.Areas.Admin.ViewModels.Topics;
using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.Services.Interfaces
{
    public interface IAdminTopicService
    {
        public Task<List<Topic>> GetAllTopicsAsync();

        public Task<Topic> GetTopicAsync(int id);

        public Task CreateTopicAsync(CreateTopicBindingModel model);

        public Task EditTopicAsync(int id, EditTopicBindingModel model);

        public Task DeleteTopicAsync(DeleteTopicViewModel model);

    }
}
