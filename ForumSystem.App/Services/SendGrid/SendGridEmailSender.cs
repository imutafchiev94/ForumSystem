using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumSystem.App.Services.SendGrid
{
    public class SendGridEmailSender : ISendGridEmailSender
    {
        private readonly SendGridOptions sendGridOptions;

        public SendGridEmailSender(IOptions<SendGridOptions> options)
        {
            sendGridOptions = options.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlmessage)
        {
            var client = new SendGridClient(this.sendGridOptions.SendGridApiKey);
            var from = new EmailAddress("i.mutafchiev94@gmail.com", "Forum System Admin");
            var to = new EmailAddress(email, email);
            var message = MailHelper.CreateSingleEmail(from, to, subject, htmlmessage, htmlmessage);
            var response = await client.SendEmailAsync(message);
            var body = await response.Body.ReadAsStringAsync();
            var statusCode = response.StatusCode;
        }
    }
}
