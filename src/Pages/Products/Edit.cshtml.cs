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
    public IFormFile? MainImage { get; set; }

    [BindProperty]
    public List<IFormFile> PhotoFiles { get; set; } = [];

    [BindProperty]
    public IFormFile? FeatureImage { get; set; } = default!;

    [BindProperty]
    public IFormFile? FeatureDataSheet { get; set; } = default!;

    [BindProperty]
    public IFormFile? ModelImage { get; set; } = default!;

    [BindProperty]
    public IFormFile? ModelDataSheet { get; set; } = default!;
    
    [BindProperty]
    public List<IFormFile> FeatureImages { get; set; } = [];

    [BindProperty]
    public List<IFormFile> FeatureDataSheets { get; set; } = [];

    [BindProperty]
    public List<IFormFile> ModelImages { get; set; } = [];

    [BindProperty]
    public List<SubModule> Features { get; set; } = [];

    [BindProperty]
    public List<SubModule> Models { get; set; } = [];
    
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
        PhotoDetails = product.Photos;
        VideoUrls = product.Videos;
        CategoryList = await context.Products.Select(p => p.Category).Distinct().ToListAsync();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
		
		product.Name = Product.Name;
		product.Description = Product.Description;
		product.Category = Product.Category;

        if (MainImage != null)
            product.Image = await FileHelper.UploadFile(MainImage, "uploads/images/products");

        for (int i = 0; i < FeatureImages.Count; i++)
            Features[i].Image = await FileHelper.UploadFile(FeatureImages[i], "uploads/images/products/features");

        for (int i = 0; i < ModelImages.Count; i++)
            Models[i].Image = await FileHelper.UploadFile(ModelImages[i], "uploads/images/products/models");

        for (int i = 0; i < FeatureDataSheets.Count; i++)
            Features[i].DataSheetUrl = await FileHelper.UploadFile(FeatureDataSheets[i], "uploads/datasheets/features");

        for (int i = 0; i < ModelDataSheets.Count; i++)
            Models[i].DataSheetUrl = await FileHelper.UploadFile(ModelDataSheets[i], "uploads/datasheets/models");

        for (int i = 0; i < PhotoFiles.Count; i++)
            PhotoDetails[i].Url = await FileHelper.UploadFile(PhotoFiles[i], "uploads/images/products");

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
        
        if (FeatureImage != null)
        {
            // TODO: Delete old image if exists
            Product.Features[featureId].Image = await FileHelper.UploadFile(FeatureImage, "uploads/images/products/features");
        }
        else
        {
            Product.Features[featureId].Image = product.Features[featureId].Image;
        }
        
        product.Features[featureId] = Product.Features[featureId];
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

        if (ModelImage != null)
        {
            // TODO: Delete old image if exists
            Product.Models[modelId].Image = await FileHelper.UploadFile(ModelImage, "uploads/images/products/models");
        }
        else
        {
            Product.Models[modelId].Image = product.Models[modelId].Image;
        }
        
        product.Models[modelId] = Product.Models[modelId];
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
