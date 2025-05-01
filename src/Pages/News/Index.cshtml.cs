using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;

namespace SmartLife.Pages.News
{
    public class IndexModel(SmartLifeDb context) : PageModel
    {
        public IList<Post> Posts { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Posts = await context.News.ToListAsync();
            return Page();
        }
    }
}
