using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Data;
using SmartLife.Models;
using SmartLife.Services;
using SmartLife.Utilities;

namespace SmartLife.Pages.Contacts;

public class IndexModel(SmartLifeDb context, EmailService emailService, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public Contact Contact { get; set; } = default!;
    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        Contact = await LocationHelper.GetContactByIpAsync(context, HttpContext) ?? new Contact();
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        var name = Request.Form["name"].ToString();
        var phone = Request.Form["phone"].ToString();
        var email = Request.Form["email"].ToString();
        var message = Request.Form["message"].ToString();

        var subject = $"Contact Form Submission from {name}";
        var body = $"Name: {name}\nPhone: {phone}\nEmail: {email}\nMessage: {message}";

        string to = Contact?.Emails[0] ?? "sales@smartlifeeg.com";

        await emailService.SendEmail(to, subject, body);

        return RedirectToPage();
    }
}
