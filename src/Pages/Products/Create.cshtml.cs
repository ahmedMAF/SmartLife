using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;
using SmartLife.Data;
using SmartLife.Utilities;

namespace SmartLife.Pages.Products;

public class CreateModel(SmartLifeDb context, IWebHostEnvironment environment) : PageModel
{
    public SelectList CategorySelectList { get; set; } = default!;

    [BindProperty]
    public Product Product { get; set; } = new();

    [BindProperty]
    public IFormFile MainImage { get; set; } = default!;

    [BindProperty]
    public List<IFormFile> AdditionalPhotos { get; set; } = [];

    [BindProperty]
    public List<string> VideoUrls { get; set; } = [];

    public async Task<IActionResult> OnGetAsync()
    {
        CategorySelectList = new SelectList(await context.Categories.ToListAsync(), "Id", "Name");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            CategorySelectList = new SelectList(await context.Categories.ToListAsync(), "Id", "Name");
            return Page();
        }
        
        string folder = Path.Combine(environment.WebRootPath, "uploads", "images", "products");
        
        if (MainImage != null)
        {
            Product.Image = await UploadHelper.UploadFile(MainImage, folder);
        }

        // Handle Additional Photos
        if (AdditionalPhotos != null)
        {
            foreach (var photo in AdditionalPhotos)
            {
                string file = await UploadHelper.UploadFile(photo, folder);
                Product.Photos.Add(new GalleryEntry { Url = file });
            }
        }

        // Handle Video URLs
        foreach (var videoUrl in VideoUrls.Where(v => !string.IsNullOrWhiteSpace(v)))
        {
            Product.Videos.Add(new GalleryEntry { Url = videoUrl });
        }

        context.Products.Add(Product);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
