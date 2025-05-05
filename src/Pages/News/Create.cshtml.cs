using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Models;

namespace SmartLife.Pages.News
{
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
            if (!ModelState.IsValid)
                return Page();
            
            // TODO: Uplaod images to a server or cloud storage and get the URLs 
            context.News.Add(Post);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
