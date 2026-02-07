using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using SmartLife.Models;
using X.PagedList;
using X.PagedList.Extensions;

namespace SmartLife.Pages.Products;

public class IndexModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public IPagedList<Product> Products { get; set; } = default!;
    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public string? CurrentCategory { get; set; }

    public IActionResult OnGet(string? id, [FromQuery] int? page)
    {
        int pageNum = page ?? 1;

        IOrderedQueryable<Product> query = context.Products
            .Where(p => id == null || (IsAr ? p.CategoryAr : p.Category) == id)
            .OrderBy(p => p.OrderIndex);

        Products = query.ToPagedList(pageNum, 10);
        CurrentCategory = id;

        return Page();
    }
}
