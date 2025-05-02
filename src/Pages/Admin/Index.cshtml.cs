using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace SmartLife.Pages.Dashboard;

public class IndexModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    [BindProperty]
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        return Page();
    }
}
