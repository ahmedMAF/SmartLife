using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;

namespace SmartLife.Pages.Team
{
    public class IndexModel(SmartLifeDb context) : PageModel
    {
        public IList<TeamMember> Team { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Team = await context.Team.ToListAsync();
        }
    }
}
