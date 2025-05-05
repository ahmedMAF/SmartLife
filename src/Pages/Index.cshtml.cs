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
    public IList<Category> Categories { get; set; } = default!;
    public Contact Contact { get; set; } = default!;
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        Partners = await context.PartnersClients.Where(m => m.Type == PcType.Partner).ToListAsync();
        Clients = await context.PartnersClients.Where(m => m.Type == PcType.Client).ToListAsync();
        Products = await context.Products.Include(p => p.Category).ToListAsync();
        Categories = await context.Categories.ToListAsync();

        Contact = await ContactHelper.GetContactByIpAsync(context, HttpContext);

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

        // Replace with your recipient email address
        var recipient = "info@smartlife.com";

        using var smtp = new System.Net.Mail.SmtpClient();
        var mail = new System.Net.Mail.MailMessage();
        mail.To.Add(recipient);
        mail.Subject = subject;
        mail.Body = body;
        mail.From = new System.Net.Mail.MailAddress(email, name);
        // Optionally set IsBodyHtml = false
        mail.IsBodyHtml = false;

        // Configure SMTP settings here if not set in appsettings.json
        // smtp.Host = "smtp.yourserver.com";
        // smtp.Port = 587;
        // smtp.Credentials = new System.Net.NetworkCredential("username", "password");
        // smtp.EnableSsl = true;

        await smtp.SendMailAsync(mail);
    }
}
