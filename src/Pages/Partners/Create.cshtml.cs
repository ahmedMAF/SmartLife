using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Data;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.Partners;

public class CreateModel(SmartLifeDb context, IWebHostEnvironment environment, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public PartnerClient Partner { get; set; } = new() { Type = PcType.Partner };

    [BindProperty]
    public IFormFile? Image { get; set; } = default!;
    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string folder = Path.Combine(environment.WebRootPath, "uploads", "images", "partners");

        if (Image != null)
            Partner.Image = await UploadHelper.UploadFile(Image, folder);

        if (!ModelState.IsValid)
            return Page();

        context.PartnersClients.Add(Partner);
        await context.SaveChangesAsync();

        return RedirectToPage("/Admin/Index");
    }
}
