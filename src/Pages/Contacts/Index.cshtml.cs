using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.Contacts
{
    public class IndexModel(SmartLifeDb context) : PageModel
    {
        public Contact Contact { get; set; } = default!;
        public IList<Contact> Contacts { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync()
        {
            Contact = await ContactHelper.GetContactByIpAsync(context, HttpContext);
            Contacts = await context.Contacts.ToListAsync();
            Contacts.Remove(Contact);

            return Page();
        }

        private struct IpApiResponse
        {
            public bool Success;
            public string CountryCode;
        }
    }
}
