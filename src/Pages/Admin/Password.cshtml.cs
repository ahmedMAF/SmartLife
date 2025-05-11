using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;

namespace SmartLife.Pages.Admin;

public class PasswordModel(SmartLifeDb context, IStringLocalizer<PasswordModel> localizer) : PageModel
{
    [BindProperty]
    public string OldPassword { get; set; }

    [BindProperty]
    public string NewPassword { get; set; }

    [BindProperty]
    public string ConfirmPassword { get; set; }

    public IStringLocalizer<PasswordModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        
        return Page();
    }

	public async Task<IActionResult> OnPostAsync()
	{
        string password = System.IO.File.ReadAllText("pwd");

        if (password != OldPassword)
        {
            ModelState.AddModelError("OldPassword", "Wrong password.");
            return Page();
        }

        if (NewPassword == ConfirmPassword)
        {
            System.IO.File.WriteAllText("pwd", NewPassword);
        }

        return RedirectToPage("/Admin/Index");
	}
}
