using Alkemy_Challenge.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alkemy_Challenge.Services
{
    public class MailService : IMailService
    {
        private readonly ISendGridClient _sendGridClient;

        public MailService(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }

        public async Task SendEmail(string name)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("alamodeg@gmail.com", "Example User"),
                Subject = "WELCOMEE!!"
            };
            msg.AddContent(MimeType.Text, $"Congratulations {name}!, you have been acepted in Alkemy Academy!! :D:D");
            msg.AddTo(new EmailAddress("alamodeg@gmail.com", "Example User"));
            var response = await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
