using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using SmartLife.Data;

namespace SmartLife.Pages.Contacts
{
    public class CreateModel(SmartLifeDb context) : PageModel
    {
        [BindProperty]
        public Contact Contact { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();
;
            context.Contacts.Add(Contact);
            await context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
