using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using SmartLife.Data;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;

namespace SmartLife.Pages.Contacts;

[Authorize]
public class CreateModel(SmartLifeDb context, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public Contact Contact { get; set; } = new();
    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        Console.WriteLine("SMD: " + Contact.Emails.Count);
        context.Contacts.Add(Contact);
        await context.SaveChangesAsync();

        return RedirectToPage("/Admin/Index");
    }
}
