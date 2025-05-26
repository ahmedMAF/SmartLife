using Microsoft.AspNetCore.Mvc;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.ViewComponents;

[ViewComponent(Name = "WhatsApp")]
public class WhatsAppViewComponent(SmartLifeDb context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("Index", await LocationHelper.GetContactByIpAsync(context, HttpContext) ?? new Contact());
    }
}