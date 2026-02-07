using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Data;
using SmartLife.Models;
using SmartLife.Pages.Admin;

namespace SmartLife.Pages.Products;

[Authorize]
public class OrderModels(SmartLifeDb context, IStringLocalizer<OrderModels> localizer) : PageModel
{
    public Product? Product { get; set; }

    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<OrderModels> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        Product = await context.Products.FindAsync(id);

        for (int i = 0; i < Product.Models.Count; i++)
        {
            SubModule model = Product.Models[i];

            if (model.OrderIndex == 0)
                model.OrderIndex = i + 1;
        }

        for (int i = 0; i < Product.Features.Count; i++)
        {
            SubModule feature = Product.Features[i];

            if (feature.OrderIndex == 0)
                feature.OrderIndex = i + 1;
        }

        Product.Features = Product.Features.OrderBy(f => f.OrderIndex).ToList();
        Product.Models = Product.Models.OrderBy(m => m.OrderIndex).ToList();

        return Page();
    }

    public async Task<IActionResult> OnPostSaveModelsOrderAsync(int productId, string ids)
    {
        Product = await context.Products.FindAsync(productId);

        var idList = ids.Split(',').Select(int.Parse).ToList();

        for (int i = 0; i < idList.Count; i++)
            Product.Models[idList[i]].OrderIndex = i + 1;

        context.Entry(Product).Property(p => p.Models).IsModified = true;

        await context.SaveChangesAsync();
        return RedirectToPage("/Products/Index");
    }
    
    public async Task<IActionResult> OnPostSaveFeaturesOrderAsync(int productId, string ids)
    {
        Product = await context.Products.FindAsync(productId);

        var idList = ids.Split(',').Select(int.Parse).ToList();

        for (int i = 0; i < idList.Count; i++)
            Product.Features[idList[i]].OrderIndex = i + 1;

        context.Entry(Product).Property(p => p.Features).IsModified = true;

        await context.SaveChangesAsync();
        return RedirectToPage("/Products/Index");
    }
}
