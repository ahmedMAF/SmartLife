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

    [BindProperty]
    public Bar GrowthBar { get; set; }

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

    public IActionResult OnPostEdit(int i)
    {
        AboutData = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));

        AboutData.Growth[i] = GrowthBar;
        System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize(AboutData));

        return RedirectToPage();
    }

    public IActionResult OnPostDel(int i)
    {
        AboutData = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));

        AboutData.Growth.RemoveAt(i);
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

    public IActionResult OnPostSince()
    {
        AboutData = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));

        AboutData.Values = Request.Form["v"];
        AboutData.ValuesAr = Request.Form["vAr"];
        AboutData.Since = Request.Form["s"];
        AboutData.SinceAr = Request.Form["sAr"];

        System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize(AboutData));

        return RedirectToPage("/Admin/Index");
    }
    
    public IActionResult OnPostPara()
    {
        AboutData = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));

        AboutData.Vision1 = Request.Form["v1"];
        AboutData.Vision1Ar = Request.Form["v1Ar"];
        AboutData.Vision2 = Request.Form["v2"];
        AboutData.Vision2Ar = Request.Form["v2Ar"];
        AboutData.Mission1 = Request.Form["m1"];
        AboutData.Mission1Ar = Request.Form["m1Ar"];
        AboutData.Mission2 = Request.Form["m2"];
        AboutData.Mission2Ar = Request.Form["m2Ar"];

        System.IO.File.WriteAllText("about.json", JsonSerializer.Serialize(AboutData));

        return RedirectToPage("/Admin/Index");
    }
}
