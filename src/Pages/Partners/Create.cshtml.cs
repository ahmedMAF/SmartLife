using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Data;
using SmartLife.Models;

namespace SmartLife.Pages.Partners
{
    public class CreateModel(SmartLifeDb context) : PageModel
    {
        [BindProperty]
        public PartnerClient Partner { get; set; } = new() { Type = PcType.Partner };

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            context.PartnersClients.Add(Partner);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
