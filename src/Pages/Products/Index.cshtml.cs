using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Models;

namespace SmartLife.Pages.Products;

[IgnoreAntiforgeryToken]
public class IndexModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public IList<Product> Products { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync()
    {
        Products = await context.Products.ToListAsync();
        return Page();
    }
}
