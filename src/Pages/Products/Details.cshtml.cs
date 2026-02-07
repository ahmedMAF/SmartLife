using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Models;

namespace SmartLife.Pages.Products;

public class DetailsModel(SmartLifeDb context, IStringLocalizer<DetailsModel> localizer) : PageModel
{
    public Product Product { get; set; } = default!;
    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<DetailsModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product == null)
            return NotFound();

        Product = product;

        Product.Features = Product.Features.OrderBy(f => f.OrderIndex).ToList();
        Product.Models = Product.Models.OrderBy(m => m.OrderIndex).ToList();

        return Page();
    }
}
