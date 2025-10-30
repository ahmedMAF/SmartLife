using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Data;
using SmartLife.Models;

namespace SmartLife.Pages.About;

public class IndexModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public AboutData AboutData { get; set; } = default!;
    public IList<Contact> Contacts { get; set; } = default!;

    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        Contacts = await context.Contacts.ToListAsync();

        if (System.IO.File.Exists("about.json"))
            AboutData = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));
        else
        {
            AboutData = new AboutData();
            System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize(AboutData));
        }

        return Page();
    }
}
