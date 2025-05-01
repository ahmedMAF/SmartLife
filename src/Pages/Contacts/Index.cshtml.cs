using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLife.Models;
using System.Net;
using System.Text.Json;

namespace SmartLife.Pages.Contacts
{
    public class IndexModel(SmartLifeDb context) : PageModel
    {
        public Contact Contact { get; set; } = default!;
        public IList<Contact> Contacts { get; set; } = default!;

        public async Task OnGetAsync()
        {
            IPAddress ip = PageContext.HttpContext.Connection.RemoteIpAddress;
            string country = "";

            using (HttpClient client = new())
            {
                HttpResponseMessage response = await client.GetAsync($"http://ip-api.com/json/{ip}?fields=16386");
                IpApiResponse c = JsonSerializer.Deserialize<IpApiResponse>(await response.Content.ReadAsStringAsync());

                country = c.Success ? c.CountryCode : "US";
            }

            Contact contact = await context.Contacts.FirstOrDefaultAsync(m => m.Country == country);

            Contact = contact ?? await context.Contacts.FirstOrDefaultAsync(m => m.Country == "eg");
            Contacts = await context.Contacts.ToListAsync();
            Contacts.Remove(contact);
        }

        private struct IpApiResponse
        {
            public bool Success;
            public string CountryCode;
        }
    }
}
