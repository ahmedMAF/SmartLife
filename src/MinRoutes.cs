using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using SmartLife.Models;
using Microsoft.AspNetCore.Localization;

namespace SmartLife;

internal class MinRoutes
{
    internal static void Map(WebApplication app)
    {
        app.MapGet("/SetLanguage", (HttpContext context, string returnUrl) =>
        {
            bool isAr = StringComparer.OrdinalIgnoreCase.Equals(System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName, "ar");

            context.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(isAr ? "en" : "ar")),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Results.Redirect(returnUrl);
        });

        app.MapGet("/Admin/Logout", async (HttpContext ctx) =>
            {
                await ctx.SignOutAsync("SLCookieAuth");
                return Results.Redirect("/Index");
            }
        );

        app.MapPost("/Partners/Delete/{id}", async (int id, SmartLifeDb ctx) =>
            {
                Console.WriteLine("SMD: " + id);
                ctx.PartnersClients.Remove(new PartnerClient { Id = id });
                await ctx.SaveChangesAsync();

                return Results.Redirect("/Partners/Index");
            }
        ).RequireAuthorization();

        
    }
}