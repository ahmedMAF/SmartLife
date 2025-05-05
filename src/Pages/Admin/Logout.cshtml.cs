using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace SmartLife.Pages.Admin;

public class LogoutModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        await HttpContext.SignOutAsync("SLCookieAuth");
        return RedirectToPage("/Index");
    }
}
