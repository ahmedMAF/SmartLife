using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace SmartLife.Pages.Partners;

public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public PartnerClient Partner { get; set; } = default!;
    
    [BindProperty]
    public IFormFile? Image { get; set; } = default!;

    [BindProperty]
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

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
