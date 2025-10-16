using Microsoft.Extensions.Options;
using SmartLife.Data;
using System.Net;
using System.Net.Mail;

namespace SmartLife.Services;

public class EmailService(IOptions<SmtpSettings> smtpOptions)
{
    private readonly SmtpSettings _smtp = smtpOptions.Value;

    public void SendEmail(string to, string from, string subject, string body)
    {
        MailMessage mail = new()
        {
            From = new MailAddress(from)
        };

        mail.To.Add(to);
        mail.Subject = subject;
        mail.Body = body;
        mail.IsBodyHtml = false;
        mail.ReplyToList.Add(new MailAddress(from));

        using var smtp = new SmtpClient(_smtp.Host, _smtp.Port)
        {
            Credentials = new NetworkCredential(to, ""), // TODO: Contact.Password
            EnableSsl = true
        };

        smtp.Send(mail);
    }
}