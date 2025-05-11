using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
    public IFormFile? Image { get; set; }

    // For editing features and models
    [BindProperty]
    public IFormFile? SubImage { get; set; } = default!;

    [BindProperty]
    public IFormFile? DataSheet { get; set; } = default!;

    [BindProperty]
    public SubModule SubModule { get; set; } = default!;

    // For Adding new features and models
    [BindProperty]
    public List<SubModule> Features { get; set; } = [];

    [BindProperty]
    public List<SubModule> Models { get; set; } = [];

    [BindProperty]
    public List<IFormFile> FeatureDataSheets { get; set; } = [];
    
    [BindProperty]
    public List<IFormFile> ModelDataSheets { get; set; } = [];

    [BindProperty]
    public List<IFormFile> FeatureImages { get; set; } = [];

    [BindProperty]
    public List<IFormFile> ModelImages { get; set; } = [];
    
    // For adding new photos and videos
    [BindProperty]
    public List<GalleryEntry> PhotoDetails { get; set; } = [];

    [BindProperty]
    public List<IFormFile> PhotoFiles { get; set; } = [];

    [BindProperty]
    public List<string> VideoUrls { get; set; } = [];

    // Others
    public List<string> CategoryList { get; set; } = default!;
    public List<string> CategoryArList { get; set; } = default!;
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        Product = product;
        CategoryList = await context.Products.Select(p => p.Category).Distinct().ToListAsync();
        CategoryArList = await context.Products.Select(p => p.CategoryAr).Distinct().ToListAsync();
        // CategoryList.Sort();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
		
		product.Name = Product.Name;
		product.NameAr = Product.NameAr;
		product.Description = Product.Description;
		product.DescriptionAr = Product.DescriptionAr;
		product.Category = Product.Category ?? "";
		product.CategoryAr = Product.CategoryAr ?? "";

        product.Image = await FileHelper.UploadReplaceFile(product.Image, Image, "uploads/images/products");

        // Newly added features and models
        for (int i = 0; i < FeatureImages.Count; i++)
            Features[i].Image = await FileHelper.UploadReplaceFile(Features[i].Image, FeatureImages[i], "uploads/images/products/features");

        for (int i = 0; i < ModelImages.Count; i++)
            Models[i].Image = await FileHelper.UploadReplaceFile(Models[i].Image, ModelImages[i], "uploads/images/products/models");

        for (int i = 0; i < FeatureDataSheets.Count; i++)
            Features[i].DataSheetUrl = await FileHelper.UploadReplaceFile(Features[i].DataSheetUrl, FeatureDataSheets[i], "uploads/datasheets/features");

        for (int i = 0; i < ModelDataSheets.Count; i++)
            Models[i].DataSheetUrl = await FileHelper.UploadReplaceFile(Models[i].DataSheetUrl, ModelDataSheets[i], "uploads/datasheets/models");

        for (int i = 0; i < PhotoFiles.Count; i++)
            PhotoDetails[i].Url = await FileHelper.UploadReplaceFile(PhotoDetails[i].Url, PhotoFiles[i], "uploads/images/products");

        foreach (var videoUrl in VideoUrls.Where(v => !string.IsNullOrWhiteSpace(v)))
            product.Videos.Add(UrlHelper.GetYouTubeVideoId(videoUrl));
            
        product.Features.AddRange(Features);
        product.Models.AddRange(Models);
        product.Photos.AddRange(PhotoDetails);
            
        await context.SaveChangesAsync();

        return RedirectToPage("/Products/Details", new { id });
    }

    public async Task<IActionResult> OnPostEditFeatureAsync(int id, int featureId)
    {
        var product = await context.Products.FindAsync(id);

        SubModule.Image = await FileHelper.UploadReplaceFile(product.Features[featureId].Image, SubImage,
            "uploads/images/products/features");
        SubModule.DataSheetUrl = await FileHelper.UploadReplaceFile(product.Features[featureId].DataSheetUrl, DataSheet,
            "uploads/datasheets/features");
        product.Features[featureId] = SubModule;

        await context.SaveChangesAsync();

        return RedirectToPage();
    }
    
    public async Task<IActionResult> OnPostDeleteFeatureAsync(int id, int featureId)
    {
        var product = await context.Products.FindAsync(id);

        product.Features.RemoveAt(featureId);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditModelAsync(int id, int modelId)
    {
        var product = await context.Products.FindAsync(id);

        SubModule.Image = await FileHelper.UploadReplaceFile(product.Models[modelId].Image, SubImage,
            "uploads/images/models/features");
        SubModule.DataSheetUrl = await FileHelper.UploadReplaceFile(product.Models[modelId].DataSheetUrl, DataSheet,
            "uploads/datasheets/models");
        product.Models[modelId] = SubModule;
        await context.SaveChangesAsync();

        return RedirectToPage();
    }
    
    public async Task<IActionResult> OnPostDeleteModelAsync(int id, int modelId)
    {
        var product = await context.Products.FindAsync(id);

        product.Models.RemoveAt(modelId);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeletePhotoAsync(int id, int photoId)
    {
        var product = await context.Products.FindAsync(id);

        product.Photos.RemoveAt(photoId);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }
    
    public async Task<IActionResult> OnPostDeleteVideoAsync(int id, int videoId)
    {
        var product = await context.Products.FindAsync(id);

        product.Videos.RemoveAt(videoId);
        await context.SaveChangesAsync();

        return RedirectToPage();
    }
}
