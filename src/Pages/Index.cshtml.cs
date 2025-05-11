using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Data;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages;

public class IndexModel(SmartLifeDb context, ILogger<IndexModel> logger, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public IList<PartnerClient> Partners { get; set; } = default!;
    public IList<PartnerClient> Clients { get; set; } = default!;
    public IList<Product> Products { get; set; } = default!;
    public IList<string> Categories { get; set; } = default!;
    public Contact Contact { get; set; } = default!;
    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        Partners = await context.PartnersClients.Where(m => m.Type == PcType.Partner).Take(6).ToListAsync();
        Clients = await context.PartnersClients.Where(m => m.Type == PcType.Client).Take(6).ToListAsync();
        Products = await context.Products.Take(6).ToListAsync();
        Categories = await context.Products.Select(p => p.Category).Distinct().ToListAsync();

        Contact = await LocationHelper.GetContactByIpAsync(context, HttpContext) ?? new Contact();

        return Page();
    }

    public async Task OnPostAsync()
    {
        var name = Request.Form["name"].ToString();
        var phone = Request.Form["phone"].ToString();
        var email = Request.Form["email"].ToString();
        var message = Request.Form["message"].ToString();

        var subject = $"Contact Form Submission from {name}";
        var body = $"Name: {name}\nPhone: {phone}\nEmail: {email}\nMessage: {message}";

        var recipient = "ceo@smartlifeeg.com";

        var mail = new MailMessage();
        mail.To.Add(recipient);
        mail.Subject = subject;
        mail.Body = body;
        mail.From = new MailAddress(email, name);
        mail.IsBodyHtml = false;
        
        EmailHelper.SendEmail(mail);
    }
}
