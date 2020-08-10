using ForumSystem.App.Areas.Admin.Services.Interfaces;
using ForumSystem.App.Areas.Admin.ViewModels.Topics;
using ForumSystem.Data;
using ForumSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.Services
{
    public class AdminTopicService : IAdminTopicService
    {
        private readonly ForumSystemDbContext _dbContext;

        public AdminTopicService(ForumSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateTopicAsync(CreateTopicBindingModel model)
        {
            var author = await _dbContext.Users.FirstOrDefaultAsync(a => a.UserName == model.Author);

            var topic = new Topic
            {
                Title = model.Title,
                Content = model.Content,
                AuthorId = author.Id,
                Author = (User)_dbContext.Users.FirstOrDefault(a => a.Id == author.Id),
                CreatedOn = model.CreatedOn
            };

            await _dbContext.Topics.AddAsync(topic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTopicAsync(DeleteTopicViewModel model)
        {
            var topic = await _dbContext.Topics.Where(t => t.IsDelete == false).FirstOrDefaultAsync(t => t.Id == model.Id);

            if(topic == null)
            {
                throw new NullReferenceException($"Topic with {model.Id} doesn't exist");
            }
            else
            {
                topic.IsDelete = true;

                _dbContext.Topics.Update(topic);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task EditTopicAsync(int id, EditTopicBindingModel model)
        {
            var topic = await GetTopicAsync(id);

            topic.Title = model.Title;
            topic.Content = model.Content;

            _dbContext.Update(topic);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Topic>> GetAllTopicsAsync()
        {
            var topics = _dbContext.Topics.Include(t => t.Author).Where(t => t.IsDelete == false).ToList();

            return topics;
        }

        public async Task<Topic> GetTopicAsync(int id)
        {
            var topic = await _dbContext.Topics.Include(t => t.Author).Include(t => t.Posts).Where(t => t.IsDelete == false).FirstOrDefaultAsync(t => t.Id == id);
            if(topic == null)
            {
                throw new NullReferenceException($"Topic with {id} doesn't exist");
            }
            else
            {
                return topic;
            }
        }
    }
}
