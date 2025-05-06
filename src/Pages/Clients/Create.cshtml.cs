using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Models;
using SmartLife.Data;

namespace SmartLife.Pages.Clients;

[Authorize]
public class CreateModel(SmartLifeDb context, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public PartnerClient Client { get; set; } = new() { Type = PcType.Client };

    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
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
