using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartLife.Data;
using SmartLife.Models;

namespace SmartLife.ViewComponents;

[ViewComponent(Name = "ProductsMenu")]
public class ProductsMenuViewComponent(SmartLifeDb context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        List<string> categories = await context.Products.Select(p => p.Category).Distinct().ToListAsync();
        var products = new Dictionary<string, IList<Product>>();

        products["-"] = await context.Products
                    .Where(p => p.Category == null)
                    .ToListAsync();
                    
        foreach (var category in categories)
        {
            products[category] = await context.Products
                .Where(p => p.Category == category)
                .ToListAsync();
        }

        return View("Index", new ProductsMenu
        {
            Products = products,
            Categories = categories
        });
    }
}