using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Models;

namespace SmartLife.Pages.Admin;

[Authorize]
public class OrderProductsModel(SmartLifeDb context, IStringLocalizer<OrderProductsModel> localizer) : PageModel
{
    public IList<Product> Products { get; set; } = null!;

    public bool IsAr { get; set; } = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");
    public IStringLocalizer<OrderProductsModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        await context.Database.ExecuteSqlRawAsync("UPDATE Products SET OrderIndex = Id WHERE OrderIndex = 0");

        Products = await context.Products.OrderBy(p => p.OrderIndex).ToListAsync();

        return Page();
    }

    public async Task<IActionResult> OnPostSaveOrderAsync(string ids)
    {
        var idList = ids.Split(',').Select(int.Parse).ToList();

        for (int i = 0; i < idList.Count; i++)
        {
            var product = await context.Products.FindAsync(idList[i]);

            if (product != null)
                product.OrderIndex = i + 1;
        }

        await context.SaveChangesAsync();
        return RedirectToPage("/Products/Index");
    }
}
