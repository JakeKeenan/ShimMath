using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using ShimMath.DTO;
using System.Threading.Tasks;

namespace ShimMathCore.BL
{
    public class EmailSenderService
    {
        public EmailSenderService(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public AuthMessageSenderOptions Options { get; } //set only via Secret Manager

        public Task<Response> SendEmailAsync(string email, string subject, string message = "", string html = "")
        {
            return Execute(Options.SendGridKey, email, subject, message, html);
        }

        public Task<Response> Execute(string apiKey, string email, string subject, string message, string html)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("hearan@shimmath.com", Options.SendGridUser),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = html
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}
