using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Models;

namespace SmartLife.Pages.Team;

[Authorize]
public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public TeamMember TeamMember { get; set; } = default!;

    [BindProperty]
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        context.Team.Update(TeamMember);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
