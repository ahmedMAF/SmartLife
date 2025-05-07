using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.News;

[Authorize]
public class CreateModel(SmartLifeDb context, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public Post Post { get; set; } = new();
    
    [BindProperty]
    public IList<IFormFile> Images { get; set; } = [];
    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string folder = "uploads/images/posts";
        Post.Time = DateTime.Now;
    
        if (Images != null)
        {
            foreach (var image in Images)
            {
                string file = await UploadHelper.UploadFile(image, folder);
                Post.Images.Add(file);
            }
        }

        if (!ModelState.IsValid)
            return Page();

        context.News.Add(Post);
        await context.SaveChangesAsync();

        return RedirectToPage("/Admin/Index");
    }
}
