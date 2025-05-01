using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Data;
using SmartLife.Models;

namespace SmartLife.Pages.Clients
{
    public class IndexModel(SmartLifeDb context) : PageModel
    {
        public IList<PartnerClient> Clients { get;set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Clients = await context.PartnersClients.Where(m => m.Type == PcType.Client).ToListAsync();
            return Page();
        }
    }
}
