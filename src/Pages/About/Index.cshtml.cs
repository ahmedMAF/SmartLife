using System.Text.Json;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Data;
using SmartLife.Models;

namespace SmartLife.Pages.About
{
    public class IndexModel : PageModel
    {
        public AboutData Data { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Data = JsonSerializer.Deserialize<AboutData>("about.json");
        }
    }
}
