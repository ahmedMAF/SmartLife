using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Utilities;

namespace SmartLife.Pages.Contacts;

[Authorize]
public class CreateModel(SmartLifeDb context, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public Contact Contact { get; set; } = new();
    
    public List<(string Code, string Name)> Countries { get; set; } = default!;
    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        Countries = LocationHelper.GetCountries();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        string embeddedUrl = UrlHelper.GetGoogleMapsEmbedUrl(Contact.GoogleMap);

        if (string.IsNullOrEmpty(embeddedUrl))
        {
            Countries = LocationHelper.GetCountries();
            ModelState.AddModelError("Contact.GoogleMap", Localizer["InvalidGoogleMapLink"]);
            return Page();
        }

        Contact.GoogleMap = embeddedUrl;
        context.Contacts.Add(Contact);
        await context.SaveChangesAsync();

        return RedirectToPage("/Admin/Index");
    }
}
