using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using SmartLife.Data;

namespace SmartLife.Pages.Team
{
    public class CreateModel(SmartLifeDb context) : PageModel
    {
        [BindProperty]
        public TeamMember TeamMember { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            context.Team.Add(TeamMember);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
