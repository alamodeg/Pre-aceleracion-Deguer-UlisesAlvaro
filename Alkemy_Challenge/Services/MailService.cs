using Alkemy_Challenge.Interfaces;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<MailService> _logger;

        public MailService(ISendGridClient sendGridClient, ILogger<MailService> logger)
        {
            _sendGridClient = sendGridClient;
            _logger = logger;
    }

        public async Task SendEmail(string name)
        {
            try
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
            catch (AggregateException aggEx)
            {
                {
                    foreach (var ex in aggEx.InnerExceptions)
                    {
                        _logger.LogError($"Caught exception: {ex.Message}");
                    }
                }
            }
        }
    }
}
