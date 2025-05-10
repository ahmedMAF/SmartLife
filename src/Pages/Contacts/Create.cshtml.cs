using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Utilities;

namespace SmartLife.Pages.Contacts;

[Authorize]
public class CreateModel(SmartLifeDb context, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public Contact Contact { get; set; } = new();
    
    public List<(string Code, string Name)> Countries { get; set; } = default!;
    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        Countries = LocationHelper.GetCountries();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        context.Contacts.Add(Contact);
        await context.SaveChangesAsync();

        return RedirectToPage("/Admin/Index");
    }
}
