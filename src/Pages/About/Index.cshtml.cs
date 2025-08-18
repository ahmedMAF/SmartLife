using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Data;

namespace SmartLife.Pages.About;

public class IndexModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public AboutData AboutData { get; set; } = default!;

    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        if (System.IO.File.Exists("about.json"))
            AboutData = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));
        else
        {
            AboutData = new AboutData();
            System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize<AboutData>(AboutData));
        }

        return Page();
    }
}
