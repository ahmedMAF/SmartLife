using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using SmartLife.Data;

namespace SmartLife.Pages.Contacts
{
    public class EditModel(SmartLifeDb context) : PageModel
    {
        [BindProperty]
        public PartnerClient Client { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Client.Type = PcType.Client;
            
            if (!ModelState.IsValid)
                return Page();

            context.PartnersClients.Add(Client);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
