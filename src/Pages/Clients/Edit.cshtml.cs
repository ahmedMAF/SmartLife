using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using SmartLife.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace SmartLife.Pages.Clients;

public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public PartnerClient Client { get; set; } = default!;

    [BindProperty]
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = await context.PartnersClients.FirstOrDefaultAsync(p => p.Id == id);

        if (client == null)
            return NotFound();

        Client = client;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        context.PartnersClients.Add(Client);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
