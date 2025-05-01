using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Data;
using SmartLife.Models;

namespace SmartLife.Pages.News
{
    public class IndexModel(SmartLifeDb context) : PageModel
    {
        public IList<Post> Posts { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Posts = await context.News.ToListAsync();
        }
    }
}
