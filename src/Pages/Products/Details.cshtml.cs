using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Models;

namespace SmartLife.Pages.Products;

public class DetailsModel(SmartLifeDb context, IStringLocalizer<DetailsModel> localizer) : PageModel
{
    public Product Product { get; set; } = default!;
    public IStringLocalizer<DetailsModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var product = await context.Products
            .Where(p => p.Id == id)
            .Include(p => p.Category)
            .FirstOrDefaultAsync();

        if (product == null)
            return NotFound();

        Product = product;
        return Page();
    }
}
