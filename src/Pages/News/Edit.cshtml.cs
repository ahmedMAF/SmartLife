using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.News;

[Authorize]
public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public Post Post { get; set; } = default!;

    [BindProperty]
    public IList<IFormFile> Images { get; set; } = [];
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var post = await context.News.FindAsync(id);

        if (post == null)
            return NotFound();

        Post = post;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string folder = "uploads/images/posts";
    
        if (Images.Count != 0)
        {
            foreach (var image in Images)
            {
                string file = await UploadHelper.UploadFile(image, folder);
                Post.Images.Add(file);
            }
        }

        if (!ModelState.IsValid)
            return Page();

        context.News.Update(Post);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
