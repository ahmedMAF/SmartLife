using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace SmartLife.Pages.Contacts;

public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public Contact Contact { get; set; } = default!;

    [BindProperty]
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(string country)
    {
        var contact = await context.Contacts.FirstOrDefaultAsync(c => c.Country == country);

        if (contact == null)
            return NotFound();

        Contact = contact;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        context.Contacts.Add(Contact);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
