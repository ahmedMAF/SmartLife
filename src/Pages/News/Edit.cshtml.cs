using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var post = await context.News.FindAsync(id);
        post.Time = Post.Time;
        post.Title = Post.Title;
        post.Content = Post.Content;

        string folder = "uploads/images/posts";
    
        if (Images.Count != 0)
        {
            foreach (var image in Images)
            {
                string file = await FileHelper.UploadFile(image, folder);
                post.Images.Add(file);
            }
        }

        await context.SaveChangesAsync();

        return RedirectToPage("/News/Index");
    }

    public async Task<IActionResult> OnPostDeletePhotoAsync(int id, int photoId)
    {
        var post = await context.News.FindAsync(id);

        post.Images.RemoveAt(photoId);
        context.News.Update(post);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }
}
