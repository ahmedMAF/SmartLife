using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Data;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.Partners;

[Authorize]
public class CreateModel(SmartLifeDb context, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public PartnerClient Partner { get; set; } = new();

    [BindProperty]
    public IFormFile? Image { get; set; } = default!;
    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Partner.Type = PcType.Partner;

        if (Image != null)
            Partner.Image = await UploadHelper.UploadFile(Image, "uploads/images/partners");

        if (!ModelState.IsValid)
            return Page();

        context.PartnersClients.Add(Partner);
        await context.SaveChangesAsync();

        return RedirectToPage("/Admin/Index");
    }
}
