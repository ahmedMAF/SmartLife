using AspNetCore.ReCaptcha;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Models;
using SmartLife.Services;
using SmartLife.Utilities;

namespace SmartLife.Pages.Contacts;

public class IndexModel(SmartLifeDb context, IReCaptchaService reCaptcha, EmailService emailService, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public Contact Contact { get; set; } = default!;
    public IList<Contact> Contacts { get; set; } = default!;

    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        Contacts = await context.Contacts.ToListAsync();

        if (string.IsNullOrEmpty(id))
            Contact = await LocationHelper.GetContactByIpAsync(context, HttpContext) ?? new Contact();
        else
            Contact = await context.Contacts.FindAsync(id);

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        bool recaptchaResult = await reCaptcha.VerifyAsync(Request.Form["g-recaptcha-response"]);

        if (!recaptchaResult)
        {
            TempData["ErrorMessage"] = Localizer["captcha"].Value;
            return RedirectToPage();
        }

        var name = Request.Form["name"].ToString();
        var phone = Request.Form["phone"].ToString();
        var email = Request.Form["email"].ToString();
        var message = Request.Form["message"].ToString();

        var subject = $"Contact Form Submission from {name}";
        var body = $"Name: {name}\nPhone: {phone}\nEmail: {email}\nMessage: {message}";

        string to = Contact?.Emails[0] ?? "sales@smartlifeeg.com";

        try
        {
            await emailService.SendEmail(to, subject, body);
            TempData["SuccessMessage"] = Localizer["senddone"].Value;
        }
        catch (Exception)
        {
            TempData["ErrorMessage"] = Localizer["senderror"].Value;
        }

        return RedirectToPage();
    }
}
