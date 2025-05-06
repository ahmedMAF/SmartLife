using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Data;

namespace SmartLife.Pages.About;

public class EditModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    [BindProperty]
    public AboutData AboutData { get;set; } = default!;
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

    public IActionResult OnPost()
    {
        System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize<AboutData>(AboutData));

        return RedirectToPage("/Admin/Index");
    }
}
