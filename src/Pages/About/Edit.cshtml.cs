using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Data;

namespace SmartLife.Pages.About;

public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public AboutData AboutData { get; set; } = default!;
    
    [BindProperty]
    public IList<Bar> Growth { get; set; }
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
        
        Growth = AboutData.Growth;

        return Page();
    }

    public IActionResult OnPostAdd(string year, string val)
    {
        AboutData = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));
        
        AboutData.Growth.Add(new(year, val));
        System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize(AboutData));

        return RedirectToPage();
    }
    
    public IActionResult OnPostEdit()
    {
        AboutData = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));
        
        AboutData.Growth = Growth;
        System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize(AboutData));

        return RedirectToPage();
    }

    public IActionResult OnPost()
    {
        AboutData = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));
        
        AboutData.Countries = Request.Form["countries"];
        AboutData.Projects = Request.Form["projects"];
        AboutData.Clients = Request.Form["clients"];
        AboutData.Consultants = Request.Form["consultants"];
        AboutData.Employees = Request.Form["employees"];
        AboutData.Branches = Request.Form["branches"];
        
        System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize(AboutData));

        return RedirectToPage("/Admin/Index");
    }
}
