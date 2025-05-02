using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Models;

namespace SmartLife.Pages.Team;

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

        context.Team.Add(TeamMember);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
