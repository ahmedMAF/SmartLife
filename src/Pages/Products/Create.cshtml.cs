using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public IFormFile? Image { get; set; } = default!;

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
    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        CategoryList = await context.Products.Select(p => p.Category).Distinct().ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Product.Category ??= "";

        if (Image != null)
            Product.Image = await UploadHelper.UploadFile(Image, "uploads/images/products");

        for (int i = 0; i < FeatureImages.Count; i++)
            Product.Features[i].Image = await UploadHelper.UploadFile(FeatureImages[i], "uploads/images/products/features");

        for (int i = 0; i < ModelImages.Count; i++)
            Product.Models[i].Image = await UploadHelper.UploadFile(ModelImages[i], "uploads/images/products/models");

        for (int i = 0; i < FeatureDataSheets.Count; i++)
            Product.Features[i].DataSheetUrl = await UploadHelper.UploadFile(FeatureDataSheets[i], "uploads/datasheets/features");

        for (int i = 0; i < ModelDataSheets.Count; i++)
            Product.Models[i].DataSheetUrl = await UploadHelper.UploadFile(ModelDataSheets[i], "uploads/datasheets/models");

        for (int i = 0; i < PhotoFiles.Count; i++)
        {
            PhotoDetails[i].Url = await UploadHelper.UploadFile(PhotoFiles[i], "uploads/images/products");
            Product.Photos.Add(PhotoDetails[i]);
        }

        foreach (var videoUrl in VideoUrls.Where(v => !string.IsNullOrWhiteSpace(v)))
            Product.Videos.Add(new GalleryEntry { Url = videoUrl.Split("v=")[1] });

        // if (!ModelState.IsValid)
        // {
        //     CategoryList = await context.Products.Select(p => p.Category).Distinct().ToListAsync();
        //     return Page();
        // }

        context.Products.Add(Product);
        await context.SaveChangesAsync();

        return RedirectToPage("/Admin/Index");
    }
}
