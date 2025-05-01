using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;

namespace SmartLife.Pages;

public class LayoutModel(SmartLifeDb context) : PageModel
{
    public IDictionary<int, IList<Product>> Products { get; set; } = new Dictionary<int, IList<Product>>();
    public IList<Category> Categories { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        Categories = await context.Categories.ToListAsync();

        Products[-1] = await context.Products
            .Where(m => m.Category == null)
            .ToListAsync();

        foreach (var cat in Categories)
        {
            Products[cat.Id] = await context.Products
                .Where(m => m.Category == cat)
                .ToListAsync();
        }

        return Page();
    }
}
