using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;
using SmartLife.Utilities;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using SmartLife.Data;
using SmartLife.Services;

namespace SmartLife.Pages.Admin;

[Authorize]
public class ScriptsModel(SmartLifeDb context, ScriptCacheService scriptCache, IStringLocalizer<ScriptsModel> localizer) : PageModel
{
    private const string Path = "scripts.json";

    [BindProperty]
    public List<ScriptModel> Scripts { get; set; } = [];

    [BindProperty]
    public List<ScriptModel> NewScripts { get; set; } = [];

    public IStringLocalizer<ScriptsModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        if (System.IO.File.Exists(Path))
            Scripts = JsonSerializer.Deserialize<List<ScriptModel>>(System.IO.File.ReadAllText(Path)) ?? [];
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Scripts.RemoveAll(s => string.IsNullOrWhiteSpace(s.Name) || string.IsNullOrWhiteSpace(s.Content));

        Scripts.AddRange(NewScripts);

        System.IO.File.WriteAllText(Path, JsonSerializer.Serialize(Scripts));
        scriptCache.LoadScriptsToCache();

        return RedirectToPage();
    }
}
