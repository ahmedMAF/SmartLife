using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;

namespace SmartLife.Pages.Products;

public class IndexModel(SmartLifeDb context) : PageModel
{
    public IList<Product> Products { get; set; } = default!;
    public IList<Category> Categories { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        Products = await context.Products
            .Include(p => p.Category)
            .ToListAsync();

        Categories = await context.Categories.ToListAsync();
        return Page();
    }
}
