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
        List<Category> categories = await context.Categories.ToListAsync();
        var products = new Dictionary<int, IList<Product>>();

        products[-1] = await context.Products
                    .Include(p => p.Category)
                    .Where(p => p.Category == null)
                    .ToListAsync();
                    
        foreach (var category in categories)
        {
            products[category.Id] = await context.Products
                .Include(p => p.Category)
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