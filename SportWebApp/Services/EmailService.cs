using MailKit.Net.Smtp;
using MimeKit;

namespace SportWebApp.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(string? email)
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("Admin", "yourway.sportweb@gmail.com"));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Hello new user!";
            message.Body = new BodyBuilder { HtmlBody = "<div>Some message. In future I will write something more...</div>" }.ToMessageBody();

            SmtpClient client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 465, true);
            await client.AuthenticateAsync("yourway.sportweb@gmail.com", "lqpgtyiekczdzmit");
            await client.SendAsync(message);

            await client.DisconnectAsync(true);
            client.Dispose();
        }
    }
}
