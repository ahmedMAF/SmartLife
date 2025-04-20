using Microsoft.Extensions.Localization;
using System.Reflection.Emit;
namespace SmartLife.Controllers;

public class HomeController
{
    private readonly IStringLocalizer<string> _localizer;

    public HomeController(IStringLocalizer<string> localizer)
    {
        _localizer = localizer;
    }
}
