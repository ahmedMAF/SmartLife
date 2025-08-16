using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Data;

namespace SmartLife.Pages.Admin;

[Authorize]
public class EditChooseModel(SmartLifeDb context, IStringLocalizer<EditChooseModel> localizer) : PageModel
{
    [BindProperty]
    public IList<Question> Questions { get; set; } = [];

    [BindProperty]
    public Question Question { get; set; } = default!;

    public IStringLocalizer<EditChooseModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        if (System.IO.File.Exists("choose.json"))
            Questions = JsonSerializer.Deserialize<List<Question>>(System.IO.File.ReadAllText("choose.json"));

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (System.IO.File.Exists("choose.json"))
            Questions = JsonSerializer.Deserialize<List<Question>>(System.IO.File.ReadAllText("choose.json"));

        Questions.Add(Question);

        System.IO.File.WriteAllText("choose.json", JsonSerializer.Serialize(Questions));

        return RedirectToPage("/Index");
    }

    public async Task<IActionResult> OnPostEditAsync(int i)
    {
        if (System.IO.File.Exists("choose.json"))
            Questions = JsonSerializer.Deserialize<List<Question>>(System.IO.File.ReadAllText("choose.json"));

        if (i < Questions.Count)
            Questions[i] = Question;

        System.IO.File.WriteAllText("choose.json", JsonSerializer.Serialize(Questions));

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDelAsync(int i)
    {
        if (System.IO.File.Exists("choose.json"))
            Questions = JsonSerializer.Deserialize<List<Question>>(System.IO.File.ReadAllText("choose.json"));

        if (i < Questions.Count)
            Questions.RemoveAt(i);

        System.IO.File.WriteAllText("choose.json", JsonSerializer.Serialize(Questions));

        return RedirectToPage();
    }
}
