using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace SmartLife.Pages.Admin;

public class PasswordModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    [BindProperty]
    public string OldPassword { get; set; }

    [BindProperty]
    public string NewPassword { get; set; }

    [BindProperty]
    public string ConfirmPassword { get; set; }

    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        
        return Page();
    }

	public async Task<IActionResult> OnPostAsync()
	{
        string password = System.IO.File.ReadAllText("pwd");

        if (password == OldPassword && NewPassword == ConfirmPassword)
        {
            System.IO.File.WriteAllText("pwd", NewPassword);
        }

        return RedirectToPage("./Index");
	}
}
