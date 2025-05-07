using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Data;

namespace SmartLife.Pages.About;

public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public AboutData AboutData { get;set; } = default!;
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        if (System.IO.File.Exists("about.json"))
            AboutData = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));
        else
        {
            AboutData = new AboutData();
            System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize(AboutData));
        }

        return Page();
    }

    public IActionResult OnPostAdd(int year, int val)
    {
        AboutData.Growth.Add((year, val));
        System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize(AboutData));

        return RedirectToPage();
    }

    public IActionResult OnPost()
    {
        System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize(AboutData));

        return RedirectToPage("/Admin/Index");
    }
}
