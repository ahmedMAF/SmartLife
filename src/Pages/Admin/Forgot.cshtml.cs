using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Net.Mail;
using SmartLife.Utilities;
using SmartLife.Services;

namespace SmartLife.Pages.Admin;

public class ForgotModel(SmartLifeDb context, IStringLocalizer<ForgotModel> localizer, EmailService emailService) : PageModel
{
    public IStringLocalizer<ForgotModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        emailService.SendEmail(
            "ceo@smartlifeeg.com",
            "noreply@smartlifeeg.com",
            "Restore Password Request For SmartLife Admin",
            $"Your password is\n{System.IO.File.ReadAllText("pwd")}"
        );
        
        return Page();
    }
}
