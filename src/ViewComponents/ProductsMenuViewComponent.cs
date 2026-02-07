using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLife.Data;
using SmartLife.Models;

namespace SmartLife.ViewComponents;

[ViewComponent(Name = "ProductsMenu")]
public class ProductsMenuViewComponent(SmartLifeDb context) : ViewComponent
{
    private static bool IsAr => StringComparer.OrdinalIgnoreCase.Equals(
        CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");

    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<Product> products = await context.Products.ToListAsync();

        Dictionary<string, List<Product>> grouped = products
            .GroupBy(p => IsAr ? p.CategoryAr : p.Category)
            .ToDictionary(
                g => string.IsNullOrWhiteSpace(g.Key) ? "-" : g.Key,
                g => g.ToList()
            );

        return View("Index", new ProductsMenu
        {
            Categories = [.. grouped.Keys],
            Products = grouped
        });
    }
}
