using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Data;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.Products;

[Authorize]
public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public Product Product { get; set; } = default!;

    [BindProperty]
    public IFormFile? MainImage { get; set; }

    [BindProperty]
    public List<IFormFile> PhotoFiles { get; set; } = [];

    [BindProperty]
    public List<IFormFile> FeatureImages { get; set; } = [];

    [BindProperty]
    public List<IFormFile> FeatureDataSheets { get; set; } = [];

    [BindProperty]
    public List<IFormFile> ModelImages { get; set; } = [];

    [BindProperty]
    public List<IFormFile> ModelDataSheets { get; set; } = [];

    [BindProperty]
    public List<GalleryEntry> PhotoDetails { get; set; } = [];

    [BindProperty]
    public List<string> VideoUrls { get; set; } = [];

    public List<string> CategoryList { get; set; } = default!;
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        Product = product;
        CategoryList = await context.Products.Select(p => p.Category).Distinct().ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string folder = "uploads/images/products";

        if (MainImage != null)
        {
            // TODO: Delete old image if exists
            Product.Image = await UploadHelper.UploadFile(MainImage, folder);
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

        context.Products.Update(Product);
        await context.SaveChangesAsync();

        return RedirectToPage("/Products/Details", Product.Id);
    }

    public async Task<IActionResult> OnPostEditFeatureAsync(int id, int featId)
    {
        var product = await context.Products.FindAsync(id);

        context.Products.Update(product);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditModelAsync(int id, int modelId)
    {
        var product = await context.Products.FindAsync(id);

        context.Products.Update(product);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeletePhotoAsync(int id, int photoId)
    {
        var product = await context.Products.FindAsync(id);

        product.Photos.RemoveAt(photoId);
        context.Products.Update(product);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }
}
