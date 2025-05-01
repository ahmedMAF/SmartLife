using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Data;
using SmartLife.Models;

namespace SmartLife.Pages.Partners
{
    public class IndexModel(SmartLifeDb context) : PageModel
    {
        public IList<PartnerClient> Partners { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Partners = await context.PartnersClients.Where(m => m.Type == PcType.Partner).ToListAsync();
        }
    }
}
