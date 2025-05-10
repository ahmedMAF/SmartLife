using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartLife.Models;
using SmartLife.Utilities;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Authorization;

namespace SmartLife.Pages.Clients;

[Authorize]
public class EditModel(SmartLifeDb context, IStringLocalizer<EditModel> localizer) : PageModel
{
    [BindProperty]
    public PartnerClient Client { get; set; } = default!;
    
    [BindProperty]
    public IFormFile? Image { get; set; } = default!;
    
    public IStringLocalizer<EditModel> Localizer { get; } = localizer;

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var client = await context.PartnersClients.FindAsync(id);

        if (client == null)
            return NotFound();

        Client = client;
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int id)
    {
        var client = await context.PartnersClients.FindAsync(id);
        client.Name = Client.Name;
        client.Description = Client.Description;
    
        if (Image != null)
        {
            FileHelper.DeleteUploadedFile(client.Image);
            client.Image = await FileHelper.UploadFile(Image, "uploads/images/clients");
        }

        await context.SaveChangesAsync();

        return RedirectToPage("/Clients/Index");
    }
}
