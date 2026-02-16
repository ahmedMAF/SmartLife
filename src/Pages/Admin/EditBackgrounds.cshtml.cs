using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Utilities;

namespace SmartLife.Pages.Admin;

[Authorize]
public class EditBackgroundsModel(SmartLifeDb context, IStringLocalizer<EditBackgroundsModel> localizer) : PageModel
{
    private const string Path = "images.json";

    [BindProperty]
    public IFormFile? Image { get; set; } = default!;
    public IList<string> Images { get; set; } = [];
    public IStringLocalizer<EditBackgroundsModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        if (System.IO.File.Exists(Path))
            Images = JsonSerializer.Deserialize<List<string>>(System.IO.File.ReadAllText(Path)) ?? [];

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (System.IO.File.Exists(Path))
            Images = JsonSerializer.Deserialize<List<string>>(System.IO.File.ReadAllText(Path)) ?? [];
        else
            Images = [];

        Images.Add(await FileHelper.UploadFile(Image, "uploads/images/home"));

        System.IO.File.WriteAllText(Path, JsonSerializer.Serialize(Images));

        return RedirectToPage("/Index");
    }

    public async Task<IActionResult> OnPostDelAsync(int i)
    {
        if (System.IO.File.Exists(Path))
            Images = JsonSerializer.Deserialize<List<string>>(System.IO.File.ReadAllText(Path));
        else
            Images = [];

        if (i < Images.Count)
            Images.RemoveAt(i);

        System.IO.File.WriteAllText(Path, JsonSerializer.Serialize(Images));

        return RedirectToPage();
    }
}
