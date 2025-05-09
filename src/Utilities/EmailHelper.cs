using System;
using System.Net;
using System.Net.Mail;

namespace SmartLife.Utilities;

public static class EmailHelper
{
    public static async Task SendEmail(MailMessage email)
    {
        using var smtp = new SmtpClient();
        
        smtp.Host = "smtp.yourserver.com";
        smtp.Port = 587;
        smtp.Credentials = new NetworkCredential("username", "password");
        smtp.EnableSsl = true;

        await smtp.SendMailAsync(email);
    }
}
