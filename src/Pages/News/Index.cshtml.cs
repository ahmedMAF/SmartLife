using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Models;

namespace SmartLife.Pages.News
{
    public class IndexModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
    {
        public IList<Post> Posts { get;set; } = default!;
        public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

        public async Task<IActionResult> OnGetAsync()
        {
            Posts = await context.News.ToListAsync();
            return Page();
        }
    }
}
