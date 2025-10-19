using Microsoft.Extensions.Options;
using SmartLife.Data;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SmartLife.Services;

public class EmailService(IOptions<SmtpSettings> smtpOptions)
{
    private readonly SmtpSettings _smtp = smtpOptions.Value;

    public async Task SendEmail(string to, string subject, string body)
    {
        MailMessage mail = new()
        {
            From = new MailAddress(_smtp.Email)
        };

        mail.To.Add(to);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = false;
        // mail.ReplyToList.Add(new MailAddress(_smtp.Email));

        using var smtp = new SmtpClient(_smtp.Host, _smtp.Port)
        {
            Credentials = new NetworkCredential(_smtp.Email, _smtp.Password),
            EnableSsl = true
        };

        smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);

        await smtp.SendMailAsync(mail);
    }

    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        string token = (string)e.UserState;

        if (e.Cancelled)
        {
            Console.WriteLine("[{0}] Send canceled.", token);
        }
        if (e.Error != null)
        {
            Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
        }
        else
        {
            Console.WriteLine("Message sent.");
        }
    }
}