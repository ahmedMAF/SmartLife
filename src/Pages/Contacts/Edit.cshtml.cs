using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Utilities;

namespace SmartLife.Pages.Contacts;

[Authorize]
public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public Contact Contact { get; set; } = default!;
    public List<(string Code, string Name)> Countries { get; set; } = default!;
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        Countries = LocationHelper.GetCountries();
        var contact = await context.Contacts.FindAsync(id);

        if (contact == null)
            return NotFound();

        Contact = contact;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        context.Contacts.Update(Contact);
        await context.SaveChangesAsync();

        return RedirectToPage("/Admin/Index");
    }
}
