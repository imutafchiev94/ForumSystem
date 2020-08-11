using ForumSystem.App.Services.Interface;
using ForumSystem.Data;
using ForumSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Services
{
    public class TopicsService : ITopicsServices
    {
        private readonly ForumSystemDbContext _dbContext;

        public TopicsService(ForumSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Topic> GetTopicAsync(int id)
        {
            var topic = await _dbContext.Topics.Include(t => t.Posts).Include(t => t.Author).FirstOrDefaultAsync(t => t.Id == id);

            if (topic == null)
            {
                throw new NullReferenceException($"Topic with {id} doesn't exist");
            }

            return topic;
        }
    }
}
