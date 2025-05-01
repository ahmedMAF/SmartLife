using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using Microsoft.EntityFrameworkCore;

namespace SmartLife.Pages.Partners;

public class EditModel(SmartLifeDb context) : PageModel
{
    [BindProperty]
    public PartnerClient Partner { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var partner = await context.PartnersClients.FirstOrDefaultAsync(p => p.Id == id);

        if (partner == null)
            return NotFound();

        Partner = partner;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        context.PartnersClients.Add(Partner);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
