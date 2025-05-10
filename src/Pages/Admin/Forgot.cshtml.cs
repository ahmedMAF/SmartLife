using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Net.Mail;
using SmartLife.Utilities;

namespace SmartLife.Pages.Admin;

public class ForgotModel(SmartLifeDb context, IStringLocalizer<ForgotModel> localizer) : PageModel
{
    public IStringLocalizer<ForgotModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        var recipient = "ceo@smartlifeeg.com";

        var mail = new MailMessage();
        mail.To.Add(recipient);
        mail.Subject = "Restore Password Request For SmartLife Admin";
        mail.Body = $"Your password is\n{System.IO.File.ReadAllText("pwd")}";
        mail.From = new MailAddress("noreply@smartlifeeg.com", "System");
        mail.IsBodyHtml = false;
        
        EmailHelper.SendEmail(mail);
        
        return Page();
    }
}
