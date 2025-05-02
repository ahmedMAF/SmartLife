using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Models;

namespace SmartLife.Pages.Team;

public class IndexModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    [BindProperty]
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public IList<TeamMember> Team { get;set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        Team = await context.Team.ToListAsync();
        return Page();
    }
}
