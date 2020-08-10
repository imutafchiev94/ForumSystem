﻿using ForumSystem.App.Services.Interface;
using ForumSystem.Data;
using ForumSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Services
{
    public class HomeService : IHomeServices
    {
        private readonly ForumSystemDbContext _dbContext;

        public HomeService(ForumSystemDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Topic>> GetAllTopics()
        {
            var topics = await _dbContext.Topics.Include(t => t.Author).Where(t => t.IsDelete == false).ToListAsync();

            return topics;
        }
    }
}
