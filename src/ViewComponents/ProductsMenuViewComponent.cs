using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLife.Data;
using SmartLife.Models;

namespace SmartLife.ViewComponents;

[ViewComponent(Name = "ProductsMenu")]
public class ProductsMenuViewComponent(SmartLifeDb context) : ViewComponent
{
    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    
    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<string> categories;
        Dictionary<string, IList<Product>> products;
        
        if (IsAr)
        {
         categories = await context.Products.Select(p => p.CategoryAr).Distinct().ToListAsync();
         products = new Dictionary<string, IList<Product>>
        {
            ["-"] = await context.Products
                    .Where(p => p.CategoryAr == "")
                    .ToListAsync()
        };
                    
        foreach (var category in categories)
        {
            if (category == "")
                continue;

            products[category] = await context.Products
                .Where(p => p.CategoryAr == category)
                .ToListAsync();
        }
        }
        else
        {
            categories = await context.Products.Select(p => p.Category).Distinct().ToListAsync();
        products = new Dictionary<string, IList<Product>>
        {
            ["-"] = await context.Products
                    .Where(p => p.Category == "")
                    .ToListAsync()
        };
                    
        foreach (var category in categories)
        {
            if (category == "")
                continue;

            products[category] = await context.Products
                .Where(p => p.Category == category)
                .ToListAsync();
        }
        }

        return View("Index", new ProductsMenu
        {
            Products = products,
            Categories = categories
        });
    }
}