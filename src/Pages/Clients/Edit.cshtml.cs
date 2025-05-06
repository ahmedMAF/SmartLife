using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using SmartLife.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;

namespace SmartLife.Pages.Clients;

[Authorize]
public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public PartnerClient Client { get; set; } = default!;

    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = await context.PartnersClients.FindAsync(id);

        if (client == null)
            return NotFound();

        Client = client;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        context.PartnersClients.Update(Client);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
