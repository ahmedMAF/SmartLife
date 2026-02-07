using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Models;

namespace SmartLife.Pages.Products;

public class IndexModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public IList<Product> Products { get; set; } = default!;
    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(string? id)
    {
        Products = await context.Products
            .Where(p => id == null || (IsAr ? p.CategoryAr : p.Category) == id)
            .OrderBy(p => p.OrderIndex)
            .ToListAsync();

        return Page();
    }
}
