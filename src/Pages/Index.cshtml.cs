using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Data;
using SmartLife.Models;
using SmartLife.Pages.Contacts;

namespace SmartLife.Pages;

public class IndexModel(SmartLifeDb context, ILogger<IndexModel> logger) : PageModel
{
    public IList<PartnerClient> Partners { get; set; } = default!;
    public IList<PartnerClient> Clients { get; set; } = default!;
    public IList<Product> Products { get; set; } = default!;
    public IList<Category> Categories { get; set; } = default!;
    public Contact Contact { get; set; } = default!;

    public async Task OnGetAsync()
    {
        Partners = await context.PartnersClients.Where(m => m.Type == PcType.Partner).ToListAsync();
        Clients = await context.PartnersClients.Where(m => m.Type == PcType.Client).ToListAsync();
        Products = await context.Products.Include(p => p.Category).ToListAsync();
        Categories = await context.Categories.ToListAsync();

        Contact = await ContactHelper.GetContactByIpAsync(context, HttpContext);
    }

    public async Task OnPostAsync()
    {
        
    }
}
