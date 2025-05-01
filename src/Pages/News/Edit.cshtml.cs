using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;

namespace SmartLife.Pages.News
{
    public class EditModel(SmartLifeDb context) : PageModel
    {
        [BindProperty]
        public Post Post { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var post = await context.News.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return NotFound();

            Post = post;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            context.News.Add(Post);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
