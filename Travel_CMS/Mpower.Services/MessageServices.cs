using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Mpower.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public async Task SendEmailAsync(EmailBase emailBase)
        {
            // Plug in your email service here to send an email.
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(emailBase.senderName, emailBase.mailFrom));
            emailMessage.To.Add(new MailboxAddress("", emailBase.email));
            emailMessage.Subject = emailBase.subject;
            emailMessage.Body = new TextPart("html") { Text = emailBase.message };
            using (var client = new SmtpClient())
            {
                //client.LocalDomain = emailBase.LocalDomain;
                await client.ConnectAsync(emailBase.smtpUrl, emailBase.port, SecureSocketOptions.Auto).ConfigureAwait(false);
               // client.Authenticate(emailBase.mailFrom,emailBase.password);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}
