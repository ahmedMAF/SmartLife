using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Data;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.Products;

public class EditModel(SmartLifeDb context, IWebHostEnvironment environment, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public Product Product { get; set; } = default!;

    [BindProperty]
    public IFormFile? MainImage { get; set; }

    [BindProperty]
    public List<IFormFile> AdditionalPhotos { get; set; } = [];
    
    [BindProperty]
    public List<string> VideoUrls { get; set; } = [];

    public SelectList CategorySelectList { get; set; } = default!;

    [BindProperty]
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
            return NotFound();

        Product = product;
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
            // Delete old image if exists
            if (!string.IsNullOrEmpty(Product.Image))
            {
                var oldImagePath = Path.Combine(environment.WebRootPath, Product.Image.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);
            }

            Product.Image = await UploadHelper.UploadFile(MainImage, folder);
        }

        if (AdditionalPhotos != null && AdditionalPhotos.Count != 0)
        {
            // Delete old additional photos if they exist
            // if (!string.IsNullOrEmpty(Product.Photos))
            // {
            //     var oldPhotos = Product.AdditionalPhotos.Split(',');
            //     foreach (var oldPhoto in oldPhotos)
            //     {
            //         var oldPhotoPath = Path.Combine(environment.WebRootPath, oldPhoto.TrimStart('/'));
            //         if (System.IO.File.Exists(oldPhotoPath))
            //         {
            //             System.IO.File.Delete(oldPhotoPath);
            //         }
            //     }
            // }

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

        context.Attach(Product).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
