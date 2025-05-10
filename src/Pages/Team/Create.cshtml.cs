using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.Team;

public class CreateModel(SmartLifeDb context, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public TeamMember TeamMember { get; set; } = default!;

    [BindProperty]
    public IFormFile? Image { get; set; } = default!;

    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (Image != null)
            TeamMember.Photo = await FileHelper.UploadFile(Image, "uploads/images/team");

        // if (!ModelState.IsValid)
        //     return Page();

        context.Team.Add(TeamMember);
        await context.SaveChangesAsync();

        return RedirectToPage("/Team/Index");
    }
}
