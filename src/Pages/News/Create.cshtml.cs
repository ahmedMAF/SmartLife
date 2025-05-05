using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.News
{
    public class CreateModel(SmartLifeDb context, IWebHostEnvironment environment, IStringLocalizer<CreateModel> localizer) : PageModel
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
            string folder = Path.Combine(environment.WebRootPath, "uploads", "images", "posts");
        
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

            context.News.Add(Post);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
