using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;
using SmartLife.Data;
using SmartLife.Utilities;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;

namespace SmartLife.Pages.Products;

[Authorize]
public class CreateModel(SmartLifeDb context, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public Product Product { get; set; } = new();

    [BindProperty]
    public IFormFile MainImage { get; set; } = default!;

    // TODO: add name and desc to photo
    [BindProperty]
    public List<IFormFile> AdditionalPhotos { get; set; } = [];

    [BindProperty]
    public List<GalleryEntry> Photos { get; set; } = [];

    [BindProperty]
    public List<string> VideoUrls { get; set; } = [];

    public List<string> CategoryList { get; set; } = default!;
    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        CategoryList = await context.Products.Select(p => p.Category).Distinct().ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string folder = "uploads/images/products";
        
        if (MainImage != null)
            Product.Image = await UploadHelper.UploadFile(MainImage, folder);

        // Handle Additional Photos
        if (AdditionalPhotos != null)
        {
            for (int i = 0; i < AdditionalPhotos.Count; i++)
            {
                Photos[i].Url = await UploadHelper.UploadFile(AdditionalPhotos[i], folder);
                Product.Photos.Add(Photos[i]);
            }
        }

        // Handle Video URLs
        foreach (var videoUrl in VideoUrls.Where(v => !string.IsNullOrWhiteSpace(v)))
        {
            Product.Videos.Add(new GalleryEntry { Url = videoUrl });
        }

        if (!ModelState.IsValid)
        {
            CategoryList = await context.Products.Select(p => p.Category).Distinct().ToListAsync();
            return Page();
        }

        context.Products.Add(Product);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}
