using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace SmartLife.Pages.Admin;

public class LoginModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string email = Request.Form["email"].ToString();
        string password;

        if (System.IO.File.Exists("pwd"))
        {
            password = System.IO.File.ReadAllText("pwd");
        }
        else
        {
            password = "12345";
            System.IO.File.WriteAllText("pwd", password);
        }

        if (email == "ceo@smartlife.com" && Request.Form["password"] == password)
        {
            List<Claim> claims =
            [
                new (ClaimTypes.Name, email)
            ];

            var identity = new ClaimsIdentity(claims, "SLCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("SLCookieAuth", principal);

            return RedirectToPage("/Admin/Index");
        }

        ModelState.AddModelError("", "Invalid login");
        return Page();
    }
}
