using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Models;

namespace SmartLife.Pages.Admin;

[Authorize]
public class EditContactsModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    [BindProperty]
    public IList<Contact> Contacts { get; set; } = default!;
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        Contacts = await context.Contacts.ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        context.Contacts.UpdateRange(Contacts);
        await context.SaveChangesAsync();

        return RedirectToPage("/Admin/Index");
    }
}
