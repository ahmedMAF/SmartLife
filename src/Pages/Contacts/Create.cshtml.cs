using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using SmartLife.Data;

namespace SmartLife.Pages.Contacts
{
    public class CreateModel(SmartLifeDb context) : PageModel
    {
        [BindProperty]
        public PartnerClient Client { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            Client.Type = PcType.Client;
            context.PartnersClients.Add(Client);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
