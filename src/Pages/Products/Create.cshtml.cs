using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;
using SmartLife.Data;

namespace SmartLife.Pages.Products;

public class CreateModel(SmartLifeDb context, IWebHostEnvironment environment) : PageModel
{
    public SelectList CategorySelectList { get; set; } = default!;

    [BindProperty]
    public Product Product { get; set; } = new()
    {
        Features = [],
        Models = [],
        Photos = [],
        Videos = []
    };

    [BindProperty]
    public IFormFile MainImage { get; set; } = default!;

    [BindProperty]
    public List<IFormFile> AdditionalPhotos { get; set; } = [];

    [BindProperty]
    public List<string> FeatureNames { get; set; } = [];

    [BindProperty]
    public List<string> ModelNames { get; set; } = [];

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

        if (MainImage != null)
        {
            var uploadsFolder = Path.Combine(environment.WebRootPath, "uploads", "products");
            Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + MainImage.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await MainImage.CopyToAsync(fileStream);
            }

            Product.Image = "/uploads/products/" + uniqueFileName;
        }

        // Handle Features
        foreach (var featureName in FeatureNames.Where(f => !string.IsNullOrWhiteSpace(f)))
        {
            Product.Features.Add(new SubModule { Name = featureName });
        }

        // Handle Models
        foreach (var modelName in ModelNames.Where(m => !string.IsNullOrWhiteSpace(m)))
        {
            Product.Models.Add(new SubModule { Name = modelName });
        }

        // Handle Additional Photos
        if (AdditionalPhotos != null)
        {
            var uploadsFolder = Path.Combine(environment.WebRootPath, "uploads", "products");
            
            foreach (var photo in AdditionalPhotos)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }

                Product.Photos.Add(new GalleryEntry { Url = "/uploads/products/" + uniqueFileName });
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
