using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartLife.Pages.Contacts;

public class EditModel(SmartLifeDb context) : PageModel
{
    [BindProperty]
    public Contact Contact { get; set; } = default!;

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
