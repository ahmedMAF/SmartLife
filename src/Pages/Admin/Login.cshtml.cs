using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace SmartLife.Pages.Admin;

public class LoginModel(SmartLifeDb context, IStringLocalizer<LoginModel> localizer) : PageModel
{
    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public IStringLocalizer<LoginModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string password;

        if (System.IO.File.Exists("pwd"))
        {
            password = System.IO.File.ReadAllText("pwd");
        }
        else
        {
            password = "123456";
            System.IO.File.WriteAllText("pwd", password);
        }

        if (Email == "ceo@smartlifeeg.com" && Password == password)
        {
            List<Claim> claims =
            [
                new (ClaimTypes.Name, Email)
            ];

            var identity = new ClaimsIdentity(claims, "SLCookieAuth");
            var principal = new ClaimsPrincipal(identity);
            var prop = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync("SLCookieAuth", principal, prop);

            return RedirectToPage("/Admin/Index");
        }

        ModelState.AddModelError("", "Invalid login");
        return Page();
    }
}
