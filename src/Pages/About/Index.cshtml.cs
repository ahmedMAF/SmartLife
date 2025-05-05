using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Data;

namespace SmartLife.Pages.About;

public class IndexModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public AboutData Data { get;set; } = default!;
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        if(System.IO.File.Exists("about.json"))
            Data = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));
        else
        {
            Data = new AboutData();
            System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize<AboutData>(Data));
        }

        return Page();
    }
}
