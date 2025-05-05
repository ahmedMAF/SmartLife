using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Data;
using SmartLife.Models;

namespace SmartLife.Pages.Partners;

public class CreateModel(SmartLifeDb context, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public PartnerClient Partner { get; set; } = new() { Type = PcType.Partner };

    [BindProperty]
    public IFormFile? Image { get; set; } = default!;
    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
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
