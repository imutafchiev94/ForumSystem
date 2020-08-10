using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Services.SendGrid
{
    public interface ISendGridEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
