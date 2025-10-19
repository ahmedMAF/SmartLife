using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Data;
using SmartLife.Models;
using SmartLife.Services;
using SmartLife.Utilities;

namespace SmartLife.Pages;

public class IndexModel(SmartLifeDb context, EmailService emailService, ILogger<IndexModel> logger, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public IList<PartnerClient> Partners { get; set; } = default!;
    public IList<PartnerClient> Clients { get; set; } = default!;
    public IList<Product> Products { get; set; } = default!;
    public IList<Question> Questions { get; set; } = [];
    public IList<Post> Posts { get; set; } = default!;
    public IList<string> Categories { get; set; } = default!;
    public IList<string> Images { get; set; } = [];
    public Contact Contact { get; set; } = default!;
    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        Partners = await context.PartnersClients.Where(m => m.Type == PcType.Partner).Take(6).ToListAsync();
        Clients = await context.PartnersClients.Where(m => m.Type == PcType.Client).Take(6).ToListAsync();
        Products = await context.Products.Take(6).ToListAsync();
        Posts = await context.News.OrderByDescending(p => p.Time).Take(3).ToListAsync();
        Categories = await context.Products.Select(p => p.Category).Distinct().ToListAsync();

        if (System.IO.File.Exists("images.json"))
            Images = JsonSerializer.Deserialize<List<string>>(System.IO.File.ReadAllText("images.json"));

        if (System.IO.File.Exists("choose.json"))
            Questions = JsonSerializer.Deserialize<List<Question>>(System.IO.File.ReadAllText("choose.json"));

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

        string to = Contact?.Emails[0] ?? "sales@smartlifeeg.com";
        
        await emailService.SendEmail("adham.abo.rabie@gmail.com", subject, body);
    }
}
