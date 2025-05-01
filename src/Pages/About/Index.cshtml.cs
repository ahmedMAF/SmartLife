using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Data;

namespace SmartLife.Pages.About
{
    public class IndexModel : PageModel
    {
        public AboutData Data { get;set; } = default!;

        public IActionResult OnGet()
        {
            Data = JsonSerializer.Deserialize<AboutData>(System.IO.File.ReadAllText("about.json"));
            return Page();
        }
    }
}
