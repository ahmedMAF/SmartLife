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
        Post? post = await context.News.FindAsync(id);
        post.Time = Post.Time;
        post.Title = Post.Title;
        post.TitleAr = Post.TitleAr;
        post.Content = Post.Content;
        post.ContentAr = Post.ContentAr;

        foreach (IFormFile image in Images)
            post.Images.Add(await FileHelper.UploadFile(image, "uploads/images/posts"));

        await context.SaveChangesAsync();

        return RedirectToPage("/News/Index");
    }

    public async Task<IActionResult> OnPostDeletePhotoAsync(int id, int photoId)
    {
        var post = await context.News.FindAsync(id);

        post.Images.RemoveAt(photoId);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }
}
