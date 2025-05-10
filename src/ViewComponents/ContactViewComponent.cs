using Microsoft.AspNetCore.Mvc;
using SmartLife.Models;
using SmartLife.Utilities;

namespace SmartLife.ViewComponents;

[ViewComponent(Name = "Contact")]
public class ContactViewComponent(SmartLifeDb context) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("Index", await LocationHelper.GetContactByIpAsync(context, HttpContext) ?? new Contact());
    }
}