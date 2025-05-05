using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.Pages.Contacts;

public class IndexModel(SmartLifeDb context, IStringLocalizer<IndexModel> localizer) : PageModel
{
    public Contact Contact { get; set; } = default!;
    public IList<Contact> Contacts { get; set; } = default!;
    public IStringLocalizer<IndexModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync()
    {
        Contact = await ContactHelper.GetContactByIpAsync(context, HttpContext);
        Contacts = await context.Contacts.ToListAsync();
        Contacts.Remove(Contact);

        return Page();
    }
}
