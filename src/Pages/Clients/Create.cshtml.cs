using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;
using SmartLife.Models;
using SmartLife.Data;
using SmartLife.Utilities;

namespace SmartLife.Pages.Clients;

[Authorize]
public class CreateModel(SmartLifeDb context, IStringLocalizer<CreateModel> localizer) : PageModel
{
    [BindProperty]
    public PartnerClient Client { get; set; } = new();

    [BindProperty]
    public IFormFile? Image { get; set; } = default!;
    public IStringLocalizer<CreateModel> Localizer { get; } = localizer;

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        Client.Type = PcType.Client;

        if (Image != null)
            Client.Image = await FileHelper.UploadFile(Image, "uploads/images/clients");

        // if (!ModelState.IsValid)
        //     return Page();

        context.PartnersClients.Add(Client);
        await context.SaveChangesAsync();

        return RedirectToPage("/Clients/Index");
    }
}
