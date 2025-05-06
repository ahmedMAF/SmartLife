using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;

namespace SmartLife.Pages.Partners;

[Authorize]
public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public PartnerClient Partner { get; set; } = default!;
    
    [BindProperty]
    public IFormFile? Image { get; set; } = default!;
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var partner = await context.PartnersClients.FindAsync(id);

        if (partner == null)
            return NotFound();

        Partner = partner;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        context.PartnersClients.Update(Partner);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
