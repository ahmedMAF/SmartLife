using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.Team;

[Authorize]
public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public TeamMember TeamMember { get; set; } = default!;

    [BindProperty]
    public IFormFile? Image { get; set; } = default!;

    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var member = await context.Team.FindAsync(id);

        if (member == null)
            return NotFound();

        TeamMember = member;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var member = await context.Team.FindAsync(id);
        member.Name = TeamMember.Name;
        member.Role = TeamMember.Role;
        member.Photo = await FileHelper.UploadReplaceFile(member.Photo, Image, "uploads/images/team");

        await context.SaveChangesAsync();

        return RedirectToPage("/Team/Index");
    }
}
