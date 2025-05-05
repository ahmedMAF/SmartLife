using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace SmartLife.Pages.Admin;

public class ForgotModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        
        return Page();
    }
}
