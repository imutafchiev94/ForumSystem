﻿using ForumSystem.App.Areas.Admin.ViewModels.Comments;
using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Areas.Admin.Services.Interfaces
{
    public interface IAdminCommentsServices
    {
        public List<Comment> GetAllComments(int id);

        public Task AddCommentAsync(AddCommentBindingModel model);

        public Task<Comment> EditCommentAsync(string content, int id);

        public Task DeleteCommentAsync(int id);

        public Task<Comment> GetCommentAsync(int id);

        public Task AddReplyAsync(AddReplyBindingModel model);

        public List<Comment> GetAllReplies(int id);
    }
}
