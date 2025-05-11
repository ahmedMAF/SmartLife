using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Utilities;

namespace SmartLife.Pages.Contacts;

[Authorize]
public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public Contact Contact { get; set; } = default!;
    public List<(string Code, string Name)> Countries { get; set; } = default!;
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(string id)
    {
        Countries = LocationHelper.GetCountries();
        var contact = await context.Contacts.FindAsync(id);

        if (contact == null)
            return NotFound();

        Contact = contact;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        for (int i = 0; i < Contact.Addresses.Count; i++)
        {
            if (string.IsNullOrEmpty(Contact.Addresses[i]) && string.IsNullOrEmpty(Contact.AddressesAr[i]))
            {
                Contact.Addresses.RemoveAt(i);
                Contact.AddressesAr.RemoveAt(i);
                i--;
            }
            else if (string.IsNullOrEmpty(Contact.Addresses[i]) || string.IsNullOrEmpty(Contact.AddressesAr[i]))
            {
                ModelState.AddModelError("", "Not all addresses are written in both languages.");
                return Page();
            }
        }

        Contact.Emails.RemoveAll(string.IsNullOrEmpty);
        Contact.Phones.RemoveAll(string.IsNullOrEmpty);
        Contact.WhatsApps.RemoveAll(string.IsNullOrEmpty);

        context.Contacts.Update(Contact);
        await context.SaveChangesAsync();

        return RedirectToPage("/Admin/Index");
    }
}
